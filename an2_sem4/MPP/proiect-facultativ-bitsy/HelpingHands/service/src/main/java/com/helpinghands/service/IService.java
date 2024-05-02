package com.helpinghands.service;

import com.helpinghands.domain.*;
import com.helpinghands.repo.data.EventOrderOption;
import com.helpinghands.service.data.UserInfo;

import java.util.List;


public interface IService {
    UserInfo login(String username, String password) throws ServiceException;
    void logout(String token);

    Eveniment[] getActualEvenimente();
    Utilizator createAccount(String username, String password, String email, String nume, String prenume) throws ServiceException;
    Iterable<Interest> getInterests();

    Interest getInterestByName(String name) throws ServiceException;

    Voluntar getVoluntarById(Integer id) throws ServiceException;

    Participant getParticipantById(Integer id) throws ServiceException;
    Admin getAdminById(Integer id) throws ServiceException;

    Eveniment getEvenimentById(Integer id) throws ServiceException;

    void addVoluntarInterest(Voluntar voluntar, Interest interest) throws ServiceException;
    void removeVoluntarInterest(Voluntar voluntar, Interest interest) throws ServiceException;
    Eveniment addEvent(Eveniment e) throws ServiceException;

    Participant addVolunteer(Voluntar voluntar, Eveniment event) throws ServiceException;
    Participant addOrganizer(Voluntar voluntar, Eveniment event) throws ServiceException;

    Eveniment deleteParticipantFromEveniment(Participant participant, Eveniment eveniment);

    UserSession getUserSession(String token) throws ServiceException;

    Eveniment[] getOrderedEveniments(EventOrderOption orderOption, int page, int itemsPerPage);
    Eveniment[] getByInterestOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage, String interest);
    Participant[] getParticipants(Eveniment event) throws ServiceException;
    Participant[] getOrganizers(Eveniment event);
    Participant[] getVolunteers(Eveniment event);

    Eveniment updateEveniment(Eveniment eveniment);

    Eveniment[] getEvenimentByOrganizerId(Integer initiatorId);
    Eveniment[] getEvenimentByVoluntarId(Integer volId);
    Post addPost(Post post) throws ServiceException;
    Iterable<Interest> getVoluntarInterest(Integer volId) throws ServiceException;
    CerereSponsor applyForSponsorship(CerereSponsor cerereSponsor);
    SponsorType getSponsorTypeByName(String name) throws ServiceException;
    Iterable<SponsorType> getSponsorTypes();
    CerereSponsor[] getPendingSponsorRequests();

    CerereSponsor addCerereSponsor(CerereSponsor cerereSponsor);

    CerereSponsor updateCerereSponsor(CerereSponsor cerereSponsor);

    CerereSponsor getCerereSponsorById(Integer id);

    List<Post> getPostsOfVoluntar(Integer volId);

    List<Post> getAllPosts();


    byte[] getProfilePic(int userId) throws ServiceException;

    void setProfilePic(int userId, byte[] bytes) throws ServiceException;

    void modifyExpPoints(Voluntar vol, Integer amount);

    UserInfo resetPassword(String username, String password);

    Utilizator getUserByName(String username) throws ServiceException;



    List<Post> getNewestPosts();

}