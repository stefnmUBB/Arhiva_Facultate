package com.example.pw2;

import com.example.pw2.domain.Comment;
import com.example.pw2.domain.ForbiddenWord;
import com.example.pw2.domain.Game;
import com.example.pw2.domain.User;

import java.sql.ResultSet;
import java.sql.SQLException;

public class ResultDecoder {
    public static <E> E decode(ResultSet result, Class<E> classType) throws Exception {
        if(classType == User.class){
            var id = result.getInt("id");
            var username = result.getString("username");
            var password = result.getString("password");
            return (E)(new User(username, password).setId(id));
        }
        if(classType == Comment.class){
            var id = result.getInt("id");
            var name = result.getString("name");
            var content = result.getString("content");
            var approved = result.getInt("approved");
            return (E)(new Comment(name, content, approved).setId(id));
        }
        if(classType == ForbiddenWord.class){
            var expr = result.getString("expression");
            return (E)(new ForbiddenWord(expr));
        }
        if(classType == Game.class){
            var id=result.getInt("game_id");
            var board=result.getString("board");
            var player1=result.getInt("player1");
            var player2=result.getInt("player2");
            var g=new Game(board, player1, player2);
            g.setGameId(id);
            return (E)g;
        }

        throw new Exception("Failed to decode " + classType.getName());
    }

}
