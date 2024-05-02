package com.example.pw2.pb3;

import com.example.pw2.Service;
import com.example.pw2.domain.ForbiddenWord;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb3AddExpr", value = "/pb3/add_expr")
public class AddExprServlet extends HttpServlet {
    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        var expr = request.getParameter("expr");
        Service.addForbiddenExpression(expr);
        PrintWriter out = response.getWriter();
        out.println("OK");
    }
}
