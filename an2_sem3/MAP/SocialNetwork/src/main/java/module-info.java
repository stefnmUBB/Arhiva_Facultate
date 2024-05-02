module sn.socialnetwork {
    requires java.sql;
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;

    opens sn.socialnetwork.domain to javafx.fxml;
    exports sn.socialnetwork.domain;

    opens sn.socialnetwork.service to javafx.fxml;
    exports sn.socialnetwork.service;

    opens sn.socialnetwork.ui to javafx.fxml;
    exports sn.socialnetwork.ui;

    opens sn.socialnetwork.controller to javafx.fxml;
    exports sn.socialnetwork.controller;

    opens sn.socialnetwork to javafx.fxml;
    exports sn.socialnetwork;
}