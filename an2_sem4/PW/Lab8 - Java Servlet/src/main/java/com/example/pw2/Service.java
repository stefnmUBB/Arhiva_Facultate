package com.example.pw2;

import com.example.pw2.domain.Comment;
import com.example.pw2.domain.ForbiddenWord;
import com.example.pw2.domain.Game;
import com.example.pw2.domain.User;
import com.example.pw2.utils.Hash;

import java.util.List;

public class Service {
    private static Database db = Database.getInstance();
    public static User login(String username, String password){
        return db.selectFirst(User.class,
                "SELECT * FROM `Users` where `username`=? and `password`=?",
                username,
                Hash.md5(password));
    }

    public static void addComment(String name, String content){
        db.executeNonQuery("INSERT INTO `comments` (`name`, `content`) VALUES (?, ?)",
                name,
                content);
    }

    public static List<Comment> getApprovedComments(){
        return db.select(Comment.class,
                "SELECT * FROM `comments` where approved = ?", 1);
    }

    public static List<Comment> getUnapprovedComments(){
        return db.select(Comment.class,
                "SELECT * FROM `comments` where approved = ?", 0);
    }

    public static void approveComment(int id){
        db.executeNonQuery("UPDATE `comments` set `approved`=? where `id`=?",1, id);
    }

    public static void addForbiddenExpression(String expr){
        db.executeNonQuery("INSERT INTO `forbidden_words` (`expression`) VALUES (?)",expr);
    }

    public static String[] getForbiddenExpressions(){
        return db.select(ForbiddenWord.class, "SELECT * FROM `forbidden_words`")
                .stream().map(ForbiddenWord::getExpression).toArray(String[]::new);

    }

    public static Game joinPlayer(int playerId){
        var incompleteGame = db.selectFirst(Game.class, "SELECT * FROM `tictactoe` WHERE `player2`=?",0);
        if(incompleteGame!=null){
            db.executeNonQuery("UPDATE `tictactoe` set player2=?", playerId);
            return incompleteGame;
        }
        var game=new Game("         ", playerId, 0);
        var id=db.insert("INSERT INTO `tictactoe` (`board`, `player1`, player2) VALUES (?,?,?)",
                "         ", playerId, 0);
        game.setGameId(id);
        return game;
    }

    public static Game getGameById(int gameId){
        return db.selectFirst(Game.class,"SELECT * FROM `tictactoe` WHERE `game_id`=?", gameId);
    }

    public static Game playerMove(int gameId, int playerId, int pos){
        var game=getGameById(gameId);
        var board=game.getBoard().toCharArray();
        if(playerId==game.getPlayer1())
            board[pos]='X';
        else
            board[pos]='O';
        db.executeNonQuery("UPDATE `tictactoe` set `board`=? WHERE game_id=?",
                String.valueOf(board), gameId);
        game.setBoard(String.valueOf(board));
        return game;
    }
}
