package com.example.pw2.domain;

public class Comment {
    private int id;
    private String name;
    private String content;
    private int approved;

    public Comment(String name, String content, int approved) {
        this.name = name;
        this.content = content;
        this.approved = approved;
    }

    public int getId() {
        return id;
    }

    public Comment setId(int id) {
        this.id = id;
        return this;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public int getApproved() {
        return approved;
    }

    public void setApproved(int approved) {
        this.approved = approved;
    }
}
