package com.example.pw2.domain;

public class Game {
    private int gameId;
    private String board;
    private int player1;
    private int player2;

    public Game() {
    }

    public Game(String board, int player1, int player2) {
        this.board = board;
        this.player1 = player1;
        this.player2 = player2;
    }

    public int getGameId() {
        return gameId;
    }

    public void setGameId(int game_id) {
        this.gameId = game_id;
    }

    public String getBoard() {
        return board;
    }

    public void setBoard(String board) {
        this.board = board;
    }

    public int getPlayer1() {
        return player1;
    }

    public void setPlayer1(int player1) {
        this.player1 = player1;
    }

    public int getPlayer2() {
        return player2;
    }

    public void setPlayer2(int player2) {
        this.player2 = player2;
    }
}
