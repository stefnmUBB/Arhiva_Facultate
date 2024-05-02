package sn.socialnetwork.controller;

import javafx.application.Platform;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;
import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.domain.Message;
import sn.socialnetwork.domain.User;
import sn.socialnetwork.domain.validators.ValidationException;
import sn.socialnetwork.repo.EntityAlreadyExistsException;
import sn.socialnetwork.service.EntityIdNotFoundException;
import sn.socialnetwork.utils.Constants;

import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.Objects;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class UserViewController extends SocialNetworkController {
    User user;

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;

        nameTextField.setText(user.getFirstName()+" "+user.getLastName());
        emailTextField.setText(user.getEmail());
        ageTextField.setText(user.getAge().toString());

        refreshFriends();
        refreshRequests();
    }

    void refreshFriends(){
        friends.clear();
        var friendsof = getNetwork().getFriendsOf(user);
        //System.out.println(friendsof.size());
        friends.addAll(friendsof);
    }

    void refreshRequests() {
        requests.clear();
        var reqs = getNetwork().getFriendRequestsOf(user);
        requests.addAll(reqs);
    }

    @FXML
    TabPane tabControl;

    @FXML
    TextField nameTextField;

    @FXML
    TextField emailTextField;

    @FXML
    TextField ageTextField;

    void initTabsUI() {
        tabControl.getTabs().forEach(tab->{
            Platform.runLater(() -> {
                if(tab.getGraphic()==null) return;
                Parent tabContainer = tab.getGraphic().getParent().getParent();
                tabContainer.setRotate(90);
                tabContainer.setTranslateY(-100);
            });
        });
    }

    void initFriendsTable() {
        friendNameColumn.setCellValueFactory(
                new PropertyValueFactory<User, String>("firstName"));
        friendSurnameColumn.setCellValueFactory(
                new PropertyValueFactory<User, String>("lastName"));
        friendAgeColumn.setCellValueFactory(
                new PropertyValueFactory<User, Integer>("age"));
        friendSinceColumn.setCellValueFactory(p -> {
            User u = p.getValue();

            LocalDateTime date = StreamSupport.stream(getNetwork().getAllFriendships().spliterator(),false)
                    .filter(f->f.containsUser(user.getId()) && f.containsUser(u.getId()))
                    .map(Friendship::getFriendsFrom)
                    .findFirst().orElse(null);
            if(date==null)
                return new SimpleStringProperty("");
            return new SimpleStringProperty(date.format(Constants.DATE_TIME_FORMATTER));
        });

        friendsTable.setItems(friends);
    }

    void initDiscover() {
        discNameColumn.setCellValueFactory(
                new PropertyValueFactory<User, String>("firstName"));
        discSurnameColumn.setCellValueFactory(
                new PropertyValueFactory<User, String>("lastName"));

        discTable.setItems(discUsers);

        searchTextField.textProperty().addListener(o->discoverFilter());
    }

    void initRequestsTable() {
        fshipUserNameColumn.setCellValueFactory(p -> {
            Friendship f = p.getValue();
            User friend = getNetwork().getUserById(f.getTheOtherOne(user.getId()));
            return new SimpleStringProperty(friend.getLastName()+" "+friend.getFirstName());
        });

        fshipSentColumn.setCellValueFactory(p -> {
            Friendship f = p.getValue();
            return new SimpleStringProperty(f.getFriendsFrom().format(Constants.DATE_TIME_FORMATTER));
        });

        fshipStatusColumn.setCellValueFactory(p -> {
            Friendship f = p.getValue();
            if(f.isSender(user.getId())) {
                return new SimpleStringProperty("Outcome");
            }
            return new SimpleStringProperty("Income");
        });

        fshipsTable.setItems(requests);
    }

    @FXML
    protected void initialize() {
        initTabsUI();
        initFriendsTable();
        initDiscover();
        initRequestsTable();
        initChatFriendsTable();
        initChatListView();
    }


    @FXML
    TableColumn<User, String> friendNameColumn;
    @FXML
    TableColumn<User, String> friendSurnameColumn;
    @FXML
    TableColumn<User, Integer> friendAgeColumn;
    @FXML
    TableColumn<User, String> friendSinceColumn;
    @FXML
    TableView<User> friendsTable;

    ObservableList<User> friends = FXCollections.observableArrayList();

    @FXML
    TextField searchTextField;

    @FXML
    TableView<User> discTable;

    @FXML
    TableColumn<User, String> discSurnameColumn;

    @FXML
    TableColumn<User, String> discNameColumn;

    ObservableList<User> discUsers = FXCollections.observableArrayList();

    void discoverFilter() {
        discAddFriendButton.setDisable(true);
        String[] keys = Arrays.stream(searchTextField.getText().split(" "))
                .map(String::toLowerCase).toArray(String[]::new);
        Predicate<User> pNotEmpty = u-> keys.length>1 || (keys.length==1 && keys[0].length()>0);
        Predicate<User> pName =u-> Arrays.stream(keys)
                .anyMatch(s->u.getFirstName().toLowerCase().contains(s));
        Predicate<User> pSurname =u-> Arrays.stream(keys)
                .anyMatch(s->u.getLastName().toLowerCase().contains(s));
        discUsers.setAll(StreamSupport.stream(getNetwork().getAllUsers().spliterator(),false)
                .filter(pNotEmpty.and(pName.or(pSurname)))
                .collect(Collectors.toList()));
    }

    @FXML
    Button discAddFriendButton;
    private User discSelectedUser;

    public void discTableMouseClicked() {
        discSelectedUser = discTable.getSelectionModel().getSelectedItem();
        if(discSelectedUser==null) {
            discAddFriendButton.setDisable(true);
            return;
        }
        discAddFriendButton.setDisable(false);
    }

    public void discAddFriendButtonClicked() {
        Friendship f = new Friendship(user.getId(), discSelectedUser.getId(),
                LocalDateTime.now());
        f.setSender(user.getId());
        try {
            getNetwork().addFriendship(f);
            refreshFriends();
            refreshRequests();
        } catch (EntityAlreadyExistsException e) {
            showErrorBox("Friendship already exists");
        }
    }

    @FXML
    TableView<Friendship> fshipsTable;
    @FXML
    TableColumn<Friendship, String> fshipUserNameColumn;
    @FXML
    TableColumn<Friendship, String> fshipSentColumn;
    @FXML
    TableColumn<Friendship, String> fshipStatusColumn;

    ObservableList<Friendship> requests = FXCollections.observableArrayList();

    @FXML
    Button removeFriendshipButton;

    @FXML
    Button acceptRequestButton;

    Friendship selectedFriendship = null;

    @FXML
    public void acceptRequestButtonClicked() {
        if(selectedFriendship==null) return;

        selectedFriendship.setPending(false); // accepted
        getNetwork().updateFriendship(selectedFriendship);

        refreshFriends();
        refreshRequests();
    }

    @FXML
    public void removeFriendshipButtonClicked() {
        if(selectedFriendship==null) return;
        try {
            getNetwork().removeFriendship(selectedFriendship.getId());
        } catch (EntityIdNotFoundException e) {
            showErrorBox("Friendship not found");
            return;
        }
        refreshFriends();
        refreshRequests();
    }

    @FXML
    public void fshipsTableClicked() {
        selectedFriendship = fshipsTable.getSelectionModel().getSelectedItem();
        acceptRequestButton.setDisable(true);
        removeFriendshipButton.setDisable(true);
        if(selectedFriendship==null) {
            return;
        }
        if(!selectedFriendship.isSender(user.getId())) {
            acceptRequestButton.setDisable(false);
        }
        removeFriendshipButton.setDisable(false);
    }

    Friendship selectedExistentFriendship = null;

    @FXML
    Button removeFriendButton;

    @FXML
    Button loginAsFriendButton;

    @FXML
    public void friendsTableClicked() {
        var selectedFriend = friendsTable.getSelectionModel().getSelectedItem();
        System.out.println("Friend = "+selectedFriend);
        if(selectedFriend == null) return;

        selectedExistentFriendship = StreamSupport.stream(getNetwork().getAllFriendships().spliterator(),false)
                .filter(f->f.containsUser(user.getId()) && f.containsUser(selectedFriend.getId()))
                .findFirst().orElse(null);

        System.out.println("Friendship = "+selectedExistentFriendship);

        removeFriendButton.setDisable(selectedExistentFriendship==null);
        loginAsFriendButton.setDisable(selectedExistentFriendship==null);
    }

    @FXML
    public void removeFriendButtonClicked() {
        if(selectedExistentFriendship==null)
            return;
        try {
            getNetwork().removeFriendship(selectedExistentFriendship.getId());
        } catch (EntityIdNotFoundException e) {
            showErrorBox("Friendship does not exist");
            return;
        }
        refreshFriends();
        refreshRequests();
    }

    @FXML
    public void logoutButtonClicked(){
        Stage loginViewStage = createStage("login-view.fxml",600,400);
        loginViewStage.setResizable(false);
        loginViewStage.show();
        getStage(friendsTable).close();
    }


    @FXML
    TableView<User> chatFriendsTable;

    @FXML
    TableColumn<User, String> chatFriendsTableNameColumn;

    @FXML
    TextField messageTextField;

    @FXML
    Button sendButton;

    User chatUser = null;

    ObservableList<Message> messages = FXCollections.observableArrayList();

    @FXML
    ListView<Message> chatListView;

    void loadChatListView() {
        messages.clear();
        if(chatUser==null) return;
        messages.addAll(getNetwork()
                .getMessagesBetween(user.getId(), chatUser.getId()));
    }

    void initChatFriendsTable() {
        chatFriendsTableNameColumn.setCellValueFactory(p-> {
            User u = p.getValue();
            return new SimpleStringProperty(u.getFullName());
        });
        chatFriendsTable.setItems(friends);
    }

    void initChatListView() {
        chatListView.setCellFactory(cell->new ListCell<Message>() {
            @Override
            protected void updateItem(Message message, boolean empty) {
                super.updateItem(message, empty);
                if (!empty && message != null) {
                    String sender = "";

                    if(Objects.equals(message.getAuthorID(), user.getId())) {
                        sender = user.getFirstName();
                    }
                    else {
                        sender = chatUser.getFirstName();
                    }

                    setText(sender + " : " +message.getContent());
                    setTooltip(new Tooltip("Sent : "+message.getDateSent()
                            .format(Constants.DATE_TIME_FORMATTER)));

                    if (Objects.equals(message.getAuthorID(), user.getId())) {
                        setStyle("-fx-font-weight: bold");
                    } else {
                        setStyle(null);
                    }
                } else {
                    setText(null);
                }
            }
        });
        chatListView.setItems(messages);
    }

    public void chatFriendsTableClicked() {
        chatUser = chatFriendsTable.getSelectionModel().getSelectedItem();
        loadChatListView();
    }

    public void sendButtonClicked() {
        try {
            Message message = new Message(user.getId(), chatUser.getId()
                    ,messageTextField.getText(), LocalDateTime.now());
            getNetwork().addMessage(message);
            messages.add(message);
            messageTextField.setText("");
        }
        catch (Exception | EntityAlreadyExistsException e) {
            showErrorBox(e.getMessage());
        }
    }

    public void loginAsFriendButtonClicked() {
        if(selectedExistentFriendship==null)
            return;
        Stage loginViewStage = createStage("login-view.fxml",600,400);

        User friend = getNetwork().getUserById(selectedExistentFriendship
                .getTheOtherOne(user.getId()));

        FXMLLoader loader = (FXMLLoader) (loginViewStage
                .getScene().getUserData());
        ((LoginViewController)(loader.getController())).setCredentials(
                friend.getEmail(),
                friend.getPassword()
        );

        loginViewStage.setResizable(false);
        loginViewStage.show();
        getStage(friendsTable).close();
    }
}
