package sn.socialnetwork.domain;

import java.time.LocalDateTime;
import java.util.Objects;

public class Message extends Entity<Long> {
    private Long authorID;
    private Long receiverID;
    private String content;
    private LocalDateTime dateSent;

    public Long getReceiverID() {
        return receiverID;
    }

    public void setReceiverID(Long receiverID) {
        this.receiverID = receiverID;
    }

    public Message(){}
    public Message(Long authorID, Long receiverID, String content, LocalDateTime dateSent) {
        this.authorID = authorID;
        this.receiverID = receiverID;
        this.content = content;
        this.dateSent = dateSent;
    }

    @Override
    public String toString() {
        return "Message{" +
                "authorID=" + authorID +
                ", content='" + content + '\'' +
                ", dateSent=" + dateSent +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Message message = (Message) o;
        return Objects.equals(authorID, message.authorID) && Objects.equals(content, message.content) && Objects.equals(dateSent, message.dateSent);
    }

    @Override
    public int hashCode() {
        return Objects.hash(authorID, content, dateSent);
    }

    public Long getAuthorID() {
        return authorID;
    }

    public void setAuthorID(Long authorID) {
        this.authorID = authorID;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public LocalDateTime getDateSent() {
        return dateSent;
    }

    public void setDateSent(LocalDateTime dateSent) {
        this.dateSent = dateSent;
    }
}
