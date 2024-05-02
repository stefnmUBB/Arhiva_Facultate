package com.helpinghands.domain;

public class Message implements IEntity {
    String text;
    Utilizator sender;
    ChatRoom chatRoom;
    public Message() {}

    public Message(String text, Utilizator sender, ChatRoom chatRoom) {
        this.text = text;
        this.sender = sender;
        this.chatRoom = chatRoom;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Utilizator getSender() {
        return sender;
    }

    public void setSender(Utilizator sender) {
        this.sender = sender;
    }

    public ChatRoom getChatRoom() {
        return chatRoom;
    }

    public void setChatRoom(ChatRoom chatRoom) {
        this.chatRoom = chatRoom;
    }

    private Integer id;
    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
