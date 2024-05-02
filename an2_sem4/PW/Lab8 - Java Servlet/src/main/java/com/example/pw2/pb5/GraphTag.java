package com.example.pw2.pb5;

import jakarta.servlet.jsp.JspException;
import jakarta.servlet.jsp.tagext.SimpleTagSupport;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.UncheckedIOException;
import java.time.LocalDate;
import java.time.format.TextStyle;
import java.util.Arrays;
import java.util.Base64;
import java.util.Locale;

public class GraphTag extends SimpleTagSupport {
    private String ox;
    private String oy;
    private String captionX;
    private String captionY;
    private int minX;
    private int maxX;
    private int minY;
    private int maxY;

    private String color;

    public void setOx(String ox) {
        this.ox = ox;
    }

    public void setOy(String oy) {
        this.oy = oy;
    }

    public void setCaptionX(String captionX) {
        this.captionX = captionX;
    }

    public void setCaptionY(String captionY) {
        this.captionY = captionY;
    }

    public void setMinX(int minX) {
        this.minX = minX;
    }

    public void setMaxX(int maxX) {
        this.maxX = maxX;
    }

    public void setMinY(int minY) {
        this.minY = minY;
    }

    public void setMaxY(int maxY) {
        this.maxY = maxY;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public void doTag() throws JspException, IOException {
        var out = getJspContext().getOut();

        BufferedImage image = new BufferedImage(100, 100, BufferedImage.TYPE_INT_RGB);
        var g = image.createGraphics();
        g.setColor(Color.white);
        g.fillRect(0, 0, 100, 100);
        //g.setColor(Color.getColor(color));
        g.setColor(Color.RED);

        var x = Arrays.stream(ox.split(" ")).map(Integer::parseInt).toArray(Integer[]::new);
        var y = Arrays.stream(oy.split(" ")).map(Integer::parseInt).toArray(Integer[]::new);

        var x1 = (x[0]-minX)*99/(maxX-minX);
        var y1 = (y[0]-minY)*99/(maxY-minY);


        for(int i=1;i<x.length;i++){
            var x2= (x[i]-minX)*99/(maxX-minX);
            var y2= 100-(y[i]-minY)*99/(maxY-minY);
            g.drawLine(x1,y1,x2,y2);
            x1=x2;
            y1=y2;
        }

        g.setColor(Color.BLACK);
        g.drawString(captionX, 0,20);
        g.drawString(captionY, 0,40);

        var url="<img src='data:image/png;base64,"+imgToBase64String(image, "png")+"'>";
        out.print(url);

    }

    public static String imgToBase64String(final BufferedImage img, final String formatName)
    {
        final ByteArrayOutputStream os = new ByteArrayOutputStream();

        try
        {
            ImageIO.write(img, formatName, os);
            return Base64.getEncoder().encodeToString(os.toByteArray());
        }
        catch (final IOException ioe)
        {
            throw new UncheckedIOException(ioe);
        }
    }
}