<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Title</title>
    <style>
        input{
            width:50px;
            height:50px;
        }
    </style>
</head>
<body>

<p id="info">Waiting...</p>
<form onsubmit="submitHandler(event);" id="board">
    <input type="submit" id="0" onclick="pressed=0" value=" ">
    <input type="submit" id="1" onclick="pressed=1" value=" ">
    <input type="submit" id="2" onclick="pressed=2" value=" "><br/>
    <input type="submit" id="3" onclick="pressed=3" value=" ">
    <input type="submit" id="4" onclick="pressed=4" value=" ">
    <input type="submit" id="5" onclick="pressed=5" value=" "><br/>
    <input type="submit" id="6" onclick="pressed=6" value=" ">
    <input type="submit" id="7" onclick="pressed=7" value=" ">
    <input type="submit" id="8" onclick="pressed=8" value=" ">
</form>

<script>
    var board=document.getElementById("board");

    var pressed=0;

    async function submitHandler(e){
        e.preventDefault();
        var bd = "";
        for(var i=0;i<9;i++){
            bd+=document.getElementById(""+i).value;
        }

        post({"action":"playerMove", "gid":gameid, "pid":playerid, "pos":pressed})
            .then(r=> {
            })
            .catch(e=>alert(e));

        console.log("_"+bd+"_");

        return false;
    }


    async function jsonOrError(resp) {
        if (resp.ok) {
            //console.log(resp.text());
            return resp.json();
        }
        throw new Error(resp.status+" : "+ await resp.text());
    }

    var playerid=0;
    var gameid = 0;

    function doFetch(url, req){
        return fetch(url, req);
    }

    function post(data){
        return doFetch('./server', {
            method: 'POST',
            cors:"no-cors",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(data)
        }).then(jsonOrError);
    }


   post({"action":"playerJoin"})
    .then(r=> {
        console.log(r);
        playerid = parseInt(r.playerId);
        gameid = parseInt(r.gameId);
        document.getElementById("info").innerHTML=`PlayerId =`+playerid+`; GameId=`+gameid;
    })
    .catch(e=>alert(e));

    setTimeout(function(){
        setInterval(function(){
            post({"action":"getBoard", "gid":gameid})
                .then(r=> {
                    console.log(r);
                    var board=r.board;
                    console.log(board);
                    for(var i=0;i<9;i++){
                        var b=document.getElementById(""+i);
                        b.value=board[i];
                        b.removeAttribute("disabled");
                        if(b.value!==" "){
                            b.setAttribute("disabled",true);
                        }
                    }
                })
                .catch(e=>alert(e));

            console.log("iter");
        },1000);
    }, 5000);
</script>
</body>
</html>
