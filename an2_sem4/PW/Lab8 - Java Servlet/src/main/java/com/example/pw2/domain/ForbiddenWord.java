package com.example.pw2.domain;

public class ForbiddenWord {
    private String expression;

    public ForbiddenWord(String expression) {
        this.expression = expression;
    }

    public String getExpression() {
        return expression;
    }

    public void setExpression(String expression) {
        this.expression = expression;
    }
}
