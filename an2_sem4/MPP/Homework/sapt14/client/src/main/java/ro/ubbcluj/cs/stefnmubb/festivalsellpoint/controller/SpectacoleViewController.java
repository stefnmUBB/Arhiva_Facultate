package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller;

import javafx.application.Platform;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationSubscriber;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.Notification;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.time.LocalDate;
import java.util.Objects;
import java.util.stream.StreamSupport;

public class SpectacoleViewController extends Controller implements INotificationSubscriber, Observer {
    @FXML
    TableView<Spectacol> spectacoleTable;
    @FXML
    TableColumn<Spectacol, String> artistColumn;
    @FXML
    TableColumn<Spectacol, String> dataColumn;
    @FXML
    TableColumn<Spectacol, String> locatieColumn;
    @FXML
    TableColumn<Spectacol, String> nrLocuriDisponibileColumn;
    @FXML
    TableColumn<Spectacol, String> nrLocuriVanduteColumn;

    @FXML
    TableView<Spectacol> filteredSpectacoleTable;
    @FXML
    TableColumn<Spectacol, String> filteredArtistColumn;
    @FXML
    TableColumn<Spectacol, String> filteredOraColumn;
    @FXML
    TableColumn<Spectacol, String> filteredLocatieColumn;
    @FXML
    TableColumn<Spectacol, String> filteredNrLocuriDisponibileColumn;
    @FXML
    TableColumn<Spectacol, String> filteredNrLocuriVanduteColumn;
    @FXML
    TextField numeCumparatorBox;
    @FXML
    TextField nrLocuriDoriteBox;
    @FXML
    Button rezervaBiletButton;
    @FXML
    DatePicker datePicker;

    @FXML
    public void initialize() {
        initSpectacoleTable();
        initFilteredSpectacoleTable();

        Platform.runLater(()->{
            loadAllSpectacole();
            datePicker.setValue(LocalDate.now());
            //loadFilteredSpectacole();
        });
        Platform.runLater(this::loadAllSpectacole);
        Platform.runLater(this::loadFilteredSpectacole);
    }

    private void loadAllSpectacole() {
        try {
            spectacole.clear();
            spectacole.addAll(StreamSupport
                .stream(appService.getAllSpectacole().spliterator(),false)
                .toList());
        } catch (ServiceException e) {
            displayException(e);
        }
    }

    private void loadFilteredSpectacole() {
        try {
            filteredSpectacole.clear();
            filteredSpectacole.addAll(StreamSupport
                    .stream(appService.filterSpectacole(datePicker.getValue().atStartOfDay()).spliterator(),false)
                    .toList());
        } catch (ServiceException e) {
            displayException(e);
        }
    }

    private final ObservableList<Spectacol> spectacole = FXCollections.observableArrayList();
    private final ObservableList<Spectacol> filteredSpectacole = FXCollections.observableArrayList();

    private void initSpectacoleTable(){
        spectacoleTable.setRowFactory(tv -> new TableRow<Spectacol>() {
            @Override
            protected void updateItem(Spectacol s, boolean empty) {
                super.updateItem(s, empty);
                if (s == null)
                    setStyle("");
                else {
                    if (s.getNrLocuriDisponibile() == 0) {
                        setStyle("-fx-background-color: red;");
                    } else
                        setStyle("");
                }
            }
        });

        artistColumn.setCellValueFactory(new PropertyValueFactory<>("artist"));
        dataColumn.setCellValueFactory(p->{
            Spectacol s = p.getValue();
            var data = s.getData().format(Constants.DATE_FORMATTER);
            return new SimpleStringProperty(data);
        });
        locatieColumn.setCellValueFactory(new PropertyValueFactory<>("locatie"));
        nrLocuriDisponibileColumn.setCellValueFactory(new PropertyValueFactory<>("nrLocuriDisponibile"));
        nrLocuriVanduteColumn.setCellValueFactory(new PropertyValueFactory<>("nrLocuriVandute"));

        spectacoleTable.setItems(spectacole);
    }

    private void initFilteredSpectacoleTable(){
        filteredSpectacoleTable.setRowFactory(tv -> new TableRow<Spectacol>() {
            @Override
            protected void updateItem(Spectacol s, boolean empty) {
                super.updateItem(s, empty);
                if (s == null)
                    setStyle("");
                else {
                    if (s.getNrLocuriDisponibile() == 0) {
                        setStyle("-fx-background-color: red;");
                    } else
                        setStyle("");
                }
            }
        });

        filteredArtistColumn.setCellValueFactory(new PropertyValueFactory<>("artist"));
        filteredOraColumn.setCellValueFactory(p->{
            Spectacol s = p.getValue();
            var data = s.getData().format(Constants.TIME_FORMATTER);
            return new SimpleStringProperty(data);
        });
        filteredLocatieColumn.setCellValueFactory(new PropertyValueFactory<>("locatie"));
        filteredNrLocuriDisponibileColumn.setCellValueFactory(new PropertyValueFactory<>("nrLocuriDisponibile"));
        filteredNrLocuriVanduteColumn.setCellValueFactory(new PropertyValueFactory<>("nrLocuriVandute"));

        filteredSpectacoleTable.setItems(filteredSpectacole);
    }

    @FXML
    public void datePickerInputChanged() {
        loadFilteredSpectacole();
    }

    @FXML
    public void rezervaBiletButtonClicked() {
        try {
            var spectacol = filteredSpectacoleTable.getSelectionModel()
                    .getSelectedItem();
            if(spectacol==null){
                throw new Exception("Alegeti un spectacol");
            }
            var nume = numeCumparatorBox.getText();
            var locuri = Integer.parseInt(nrLocuriDoriteBox.getText());

            if(Objects.equals(nume, "")){
                throw new Exception("Numele nu poate fi gol");
            }
            if(locuri<=0 || locuri>spectacol.getNrLocuriDisponibile()){
                throw new Exception("Numar de locuri incorect");
            }

            appService.reserveBilet(spectacol, nume, locuri);
            Utils.showMessageBox("Success!");
            //loadAllSpectacole();
            //loadFilteredSpectacole();
        }
        catch (NumberFormatException e){
            Utils.showMessageBox("Numar de locuri invalid");
        }
        catch (BiletReservationException e) {
            displayException(e);
        }
        catch (Exception e){
            displayException(e);
            //throw new RuntimeException(e);
        }
    }

    @Override
    public void updatedSpectacol(Spectacol s) {
        Platform.runLater(()-> {
            for (var is : spectacole) {
                if (Objects.equals(is.getId(), s.getId())) {
                    spectacole.remove(is);
                    spectacole.add(s);
                    break;
                }
            }

            for (var is : filteredSpectacole) {
                if (Objects.equals(is.getId(), s.getId())) {
                    filteredSpectacole.remove(is);
                    filteredSpectacole.add(s);
                    break;
                }
            }
        });
    }

    @Override
    public void notificationReceived(Notification notif) {
        System.out.println("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        System.out.println(notif);
        Platform.runLater(()-> {
            for (var s : spectacole) {
                if (s.getId() == notif.getSpectacolId()) {
                    s.setNrLocuriDisponibile(notif.getNrLocuriDisponibile());
                    s.setNrLocuriVandute(notif.getNrLocuriVandute());
                }
            }
            spectacoleTable.refresh();

            for (var s : filteredSpectacole) {
                if (s.getId() == notif.getSpectacolId()) {
                    s.setNrLocuriDisponibile(notif.getNrLocuriDisponibile());
                    s.setNrLocuriVandute(notif.getNrLocuriVandute());
                }
            }
            filteredSpectacoleTable.refresh();
        });
    }
}
