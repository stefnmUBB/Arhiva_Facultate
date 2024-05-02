const Koa = require('koa');
const app = new Koa();
const server = require('http').createServer(app.callback());
const WebSocket = require('ws');
const wss = new WebSocket.Server({ server });
const Router = require('koa-router');
const cors = require('koa-cors');
const bodyparser = require('koa-bodyparser');

app.use(bodyparser());
app.use(cors());
app.use(async (ctx, next) => {
  const start = new Date();
  await next();
  const ms = new Date() - start;
  console.log(`${ctx.method} ${ctx.url} ${ctx.response.status} - ${ms}ms`);
});

app.use(async (ctx, next) => {
  await new Promise(resolve => setTimeout(resolve, 2000));
  await next();
});

app.use(async (ctx, next) => {
  try {
    await next();
  } catch (err) {
    ctx.response.body = { issue: [{ error: err.message || 'Unexpected error' }] };
    ctx.response.status = 500;
  }
});

const { Item, loadItems } = require("./Item.js");

var fs = require('fs');
const items = loadItems(JSON.parse(fs.readFileSync('items.json', 'utf8')));

console.log(items);

let lastUpdated = items[items.length - 1].date;
let lastId = items[items.length - 1].id;
const pageSize = 10;

const broadcast = data =>
  wss.clients.forEach(client => {
    if (client.readyState === WebSocket.OPEN) {
      client.send(JSON.stringify(data));
    }
  });

const router = new Router();

router.get('/item', ctx => {
  ctx.response.body = items;
  ctx.response.status = 200;
});

router.get('/item/:id', async (ctx) => {
  const itemId = ctx.request.params.id;
  const item = items.find(item => itemId === item.id);
  if (item) {
    ctx.response.body = item;
    ctx.response.status = 200; // ok
  } else {
    ctx.response.body = { message: `item with id ${itemId} not found` };
    ctx.response.status = 404; // NOT FOUND (if you know the resource was deleted, then return 410 GONE)
  }
});

const createItem = async (ctx) => {
  const item = ctx.request.body;
  if (!item.title) { // validation
    ctx.response.body = { message: 'Title is missing' };
    ctx.response.status = 400; //  BAD REQUEST
    return;
  }
  item.id = `${parseInt(lastId) + 1}`;
  lastId = item.id;
  item.date = new Date();
  item.version = 1;
  items.push(item);
  ctx.response.body = item;
  ctx.response.status = 201; // CREATED
  broadcast({ event: 'created', payload: { item } });
};

router.post('/item', async (ctx) => {
  await createItem(ctx);
});

router.put('/item/:id', async (ctx) => {
  const id = ctx.params.id;
  const item = ctx.request.body;
  item.date = new Date();
  const itemId = item.id;
  if (itemId && id !== item.id) {
    ctx.response.body = { message: `Param id and body id should be the same` };
    ctx.response.status = 400; // BAD REQUEST
    return;
  }
  if (!itemId) {
    await createItem(ctx);
    return;
  }
  const index = items.findIndex(item => item.id === id);
  if (index === -1) {
    ctx.response.body = { issue: [{ error: `item with id ${id} not found` }] };
    ctx.response.status = 400; // BAD REQUEST
    return;
  }
  const itemVersion = parseInt(ctx.request.get('ETag')) || item.version;
  if (itemVersion < items[index].version) {
    ctx.response.body = { issue: [{ error: `Version conflict` }] };
    ctx.response.status = 409; // CONFLICT
    return;
  }
  item.version++;
  items[index] = item;
  lastUpdated = new Date();
  ctx.response.body = item;
  ctx.response.status = 200; // OK
  broadcast({ event: 'updated', payload: { item } });
});

router.del('/item/:id', ctx => {
  console.log(`del ${ctx.params.id}`)
  const id = ctx.params.id;
  const index = items.findIndex(item => id === item.id);
  if (index !== -1) {
    const item = items[index];
    items.splice(index, 1);
    lastUpdated = new Date();

    console.log(item);
    //ctx.response.body = item;

    broadcast({ event: 'deleted', payload: { item } });
  }
  ctx.response.status = 204; // no content
});

/*setInterval(() => {
  lastUpdated = new Date();
  lastId = `${parseInt(lastId) + 1}`;
  const item = new Item({ id: lastId, title: `item ${lastId}`, date: lastUpdated, version: 1 });
  items.push(item);
  console.log(`New item: ${item.title}`);
  broadcast({ event: 'created', payload: { item } });
}, 5000);*/

app.use(router.routes());
app.use(router.allowedMethods());

server.listen(3000);
