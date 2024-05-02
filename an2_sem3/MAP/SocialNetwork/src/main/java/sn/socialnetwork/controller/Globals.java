package sn.socialnetwork.controller;

import sn.socialnetwork.service.Auth;
import sn.socialnetwork.service.Network;

public class Globals {
    private static Network network = Network.loadDefaultNetwork();

    public static Network getNetwork() {
        return network;
    }
}
