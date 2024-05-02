class Item {
    constructor({ id, title, authors, launchDate, platform, lastVersion, url, totalReleases, isOpenSource }) {
        this.id = id;
        this.title = title;
        this.launchDate = new Date(launchDate);
        this.platform = platform;
        this.lastVersion = lastVersion;
        this.url = url;      
        this.authors = authors;      
        this.totalReleases = totalReleases;
        this.isOpenSource = isOpenSource;
    }
}

function loadItems(jsonList){
    const items=[];
    for (let i = 0; i < jsonList.length; i++) 
        items.push(new Item({ 
            id: jsonList[i].id, 
            title: jsonList[i].title, 
            authors: jsonList[i].authors,
            launchDate: new Date(jsonList[i].launchDate),
            platform: jsonList[i].platform,
            lastVersion: jsonList[i].lastVersion,
            url : jsonList[i].url,           
            totalReleases: jsonList[i].totalReleases,
            isOpenSource: jsonList[i].isOpenSource
        }));
    return items;
}


module.exports={ Item:Item, loadItems:loadItems };
//export {Item, loadItems};