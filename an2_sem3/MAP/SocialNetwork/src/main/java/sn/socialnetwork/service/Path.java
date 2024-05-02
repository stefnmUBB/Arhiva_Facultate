package sn.socialnetwork.service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class Path {
    List<Long> nodes = new ArrayList<>();

    private Path() {}

    public Path(Long u, Long v) {
        nodes.add(u);
        nodes.add(v);
    }

    public Long head() {return nodes.get(0);}
    public Long tail(){ return nodes.get(nodes.size()-1);}

    public boolean canJoin(Path path) {
        if(path.length()>2) return false;
        if ( !(this.head().equals(path.tail())
            || this.tail().equals(path.head())))
            return false;

        if ( (this.head().equals(path.head())
                || this.tail().equals(path.tail())))
            return false;

        for(int i=1;i<this.nodes.size()-1;i++) {
            for(var y : path.nodes) {
                if(nodes.get(i).equals(y)) {
                    return false;
                }
            }
        }
        return true;
    }

    Path reversed() {
        Path p = new Path();
        for(int i=nodes.size()-1;i>=0;i--) {
            p.nodes.add(nodes.get(i));
        }
        return p;
    }

    public Path join(Path path) {
        if(!canJoin(path)) return null;
        Path p1, p2;
       if(this.head().equals(path.tail())) {
            p1 = path;
            p2 = this;
        }
        else if(this.tail().equals(path.head())) {
            p1 = this;
            p2 = path;
        }
        else return null;
        Path p = new Path();
        for(int i=0;i<p1.nodes.size()-1;i++) {
            p.nodes.add(p1.nodes.get(i));
        }
        p.nodes.addAll(p2.nodes);
        return p;
    }

    public String toString() {
        return nodes.stream().map(Object::toString).collect(Collectors.joining(", "));
    }
    int length() {return nodes.size();}
}
