package com.helpinghands.domain;

public class Notification implements IEntity {
    private String text;
    private Post post;
    private boolean viewed=false;

    public boolean getViewed() {
        return viewed;
    }

    public void setViewed(boolean viewed) {
        this.viewed = viewed;
    }

    public Notification() {}
    public Notification(String text, Post post) {
        this.text = text;
        this.post = post;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Post getPost() {
        return post;
    }

    public void setPost(Post post) {
        this.post = post;
    }

    private Integer id;
    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
