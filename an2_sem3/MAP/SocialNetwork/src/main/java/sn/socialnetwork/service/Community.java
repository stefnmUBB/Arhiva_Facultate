package sn.socialnetwork.service;

import java.util.*;
import java.util.stream.Collectors;

import static java.lang.Math.max;

public class Community {
    private final Network network;
    Map<Long, List<Long>> adj = new HashMap<>();

    /**
     * Creates a new community instance
     * @param network owner network
     * @param iduser a user whose community is built
     */
    public Community(Network network, Long iduser) {
        this.network = network;
        adj.put(iduser, new ArrayList<>());
        discover(iduser);
    }

    /**
     * Checks if a user belongs to the community
     * @param uid user id
     * @return true if user is in community
     */
    public boolean contains(Long uid) {
        return adj.containsKey(uid);
    }

    /**
     * DFSs the network to reveal the community
     * @param uid starting user node
     */
    private void discover(Long uid) {
        Stack<Long> st = new Stack<>();
        st.push(uid);

        while(!st.empty()) {
            Long id = st.pop();
            network.getAllFriendships().forEach(f->{
                if(f.containsUser(id)) {
                    Long otherId = f.getTheOtherOne(id);
                    //System.out.println(id+" "+otherId);

                    if(!adj.containsKey(otherId)) {
                        st.push(otherId);
                    }

                    addFriends(id, otherId);
                }
            });
        }
    }

    /**
     * retrieves longest path in the community
     * @return the value of the longest path
     */
    public int getLongestPathLength() {
        if(size()<=1) return 0;
        if(size()==2) return 1;
        List<Path> paths = new ArrayList<>();
        List<Path> store = new ArrayList<>();
        List<Path> dumper = new ArrayList<>();

        List<Path> edges = new ArrayList<>();
        List<Path> edump = new ArrayList<>();

        adj.keySet().forEach(u-> adj.get(u).stream().filter(v->u<v)
                .forEach(v->{
                    paths.add(new Path(u,v));
                    paths.add(new Path(v,u));
                    edges.add(new Path(u,v));
                    edges.add(new Path(v,u));
                }));

        System.out.println("Edges count = "+paths.size());
        int globalmax=0;
        for(int k=0;k<size();k++) {
            final int[] lenmax = {globalmax};
            paths.forEach(path-> {
                final int[] cnt = {0};
                edges.forEach(e->{
                    if (path.canJoin(e)) {
                        var p = path.join(e);
                        store.add(p);
                        System.out.println(p);
                        lenmax[0] = max(lenmax[0], p.length());
                        cnt[0]++;
                    }
                });
                if (cnt[0] == 0) {
                    dumper.add(path);
                }
            });

            edges.forEach(e->{
                final int[] cnt = {0};
                paths.forEach(p->{
                    if(p.canJoin(e)) cnt[0]++;
                });
                if(cnt[0]==0)
                    edump.add(e);
            });

            paths.clear();
            paths.addAll(store);
            store.clear();
            paths.removeAll(dumper);
            System.out.println("[DBG] Dump len = "+dumper.size());
            dumper.clear();

            edges.removeAll(edump);
            System.out.println("[DBG] Edges dump len = "+edump.size());
            edump.clear();
            if(globalmax== lenmax[0])
                break;
            globalmax= lenmax[0];

            System.out.println("[DBG] Partial max path len = "+globalmax);
            System.out.println("[DBG] New cache size = " + paths.size());
        }
        return globalmax;

    }

    /**
     * adds new edge (friendship) to the community
     * @param k one user id
     * @param i other user id
     */
    private void addItem(Long k, Long i) {
        if(!adj.containsKey(k)) {
            adj.put(k, new ArrayList<>());
        }
        if(!adj.get(k).contains(i))
            adj.get(k).add(i);
    }

    /**
     * adds two friends to the community
     * @param id1 one user id
     * @param id2 other user id
     */
    private void addFriends(Long id1, Long id2) {
        addItem(id1, id2);
        addItem(id2, id1);
    }

    /**
     * gets the community size
     * @return number of users in the community
     */
    public int size() {return adj.keySet().size();}

    public String toString() {
        return adj.keySet().stream().map(Object::toString).collect(Collectors.joining(", "));
    }

}
