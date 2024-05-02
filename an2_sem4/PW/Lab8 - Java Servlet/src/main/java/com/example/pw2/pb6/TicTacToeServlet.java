package com.example.pw2.pb6;

import com.example.pw2.Service;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.module.SimpleModule;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.*;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.Dictionary;
import java.util.Map;
import java.util.Objects;

@WebServlet(name = "pb6Server", value = "/pb6/server")
public class TicTacToeServlet extends HttpServlet {
    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        PrintWriter out = response.getWriter();
        var req=getRequest(request);
        var action = (String)req.get("action");
        System.out.println(action);
        if(Objects.equals(action, "playerJoin")){
            int playerId = LocalDateTime.now().getNano()/100 % 1000000;

            var game = Service.joinPlayer(playerId);

            out.print("{" +
                    "\"playerId\":\""+playerId+"\"," +
                    "\"gameId\":\""+game.getGameId()+"\"," +
                    "\"board\":\""+game.getBoard()+"\"" +
                    "}");
        }
        else if(Objects.equals(action, "getBoard")){
            var gid = (int)req.get("gid");
            var game=Service.getGameById(gid);
            out.print("{" +
                    "\"board\":\""+game.getBoard()+"\"" +
                    "}");
        }
        else if(Objects.equals(action, "playerMove")){
            var gid = (int)req.get("gid");
            var pid = (int)req.get("pid");
            var pos=(int)req.get("pos");
            var game=Service.playerMove(gid, pid, pos);
            out.print("{\"result\":\"OK\"}");
        }
        else {
            out.println("{\"response\":\"ERROR\"}");
        }
    }

    public static Map<String, Object> getRequest(HttpServletRequest request){
        var json = getBody(request);
        ObjectMapper mapper = new ObjectMapper();
        Map<String,Object> map = null;
        try {
            map = mapper.readValue(json, Map.class);
            return map;
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }


    }

    public static String getBody(HttpServletRequest request)  {

        String body = null;
        StringBuilder stringBuilder = new StringBuilder();
        BufferedReader bufferedReader = null;

        try {
            InputStream inputStream = request.getInputStream();
            if (inputStream != null) {
                bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                char[] charBuffer = new char[128];
                int bytesRead = -1;
                while ((bytesRead = bufferedReader.read(charBuffer)) > 0) {
                    stringBuilder.append(charBuffer, 0, bytesRead);
                }
            } else {
                stringBuilder.append("");
            }
        } catch (IOException ex) {
            // throw ex;
            return "";
        } finally {
            if (bufferedReader != null) {
                try {
                    bufferedReader.close();
                } catch (IOException ex) {

                }
            }
        }

        body = stringBuilder.toString();
        return body;
    }
}
