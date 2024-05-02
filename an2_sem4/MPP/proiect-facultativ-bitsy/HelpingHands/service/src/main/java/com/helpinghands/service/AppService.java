package com.helpinghands.service;

import com.helpinghands.domain.*;
import com.helpinghands.repo.*;
import com.helpinghands.repo.data.EventOrderOption;
import com.helpinghands.service.data.UserInfo;
import com.helpinghands.service.security.RSA;
import com.helpinghands.service.security.SHA256;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.IOException;
import java.util.*;
import java.util.stream.StreamSupport;

public class AppService implements IService {
    private final static Logger logger = LogManager.getLogger();
    private final IAdminRepo adminRepo;
    private final ICerereSponsorRepo cerereSponsorRepo;
    private final IChatRoomRepo chatRoomRepo;
    private final IEvenimentRepo evenimentRepo;
    private final IInterestRepo interestRepo;
    private final IMessageRepo messageRepo;
    private final INotificareRepo notificareRepo;
    private final IParticipantRepo participantRepo;
    private final IPostRepo postRepo;
    private final IVoluntarRepo voluntarRepo;
    private final IUserSessionRepo userSessionRepo;
    private final ISponsorTypeRepo sponsorTypeRepo;
    private final IProfilePicRepo profilePicRepo;

    public AppService(IAdminRepo adminRepo, ICerereSponsorRepo cerereSponsorRepo, IChatRoomRepo chatRoomRepo, IEvenimentRepo evenimentRepo, IInterestRepo interestRepo, IMessageRepo messageRepo, INotificareRepo notificareRepo, IParticipantRepo participantRepo, IPostRepo postRepo, IVoluntarRepo voluntarRepo, IUserSessionRepo userSessionRepo, ISponsorTypeRepo sponsorTypeRepo, IProfilePicRepo profilePicRepo) {
        this.adminRepo = adminRepo;
        this.cerereSponsorRepo = cerereSponsorRepo;
        this.chatRoomRepo = chatRoomRepo;
        this.evenimentRepo = evenimentRepo;
        this.interestRepo = interestRepo;
        this.messageRepo = messageRepo;
        this.notificareRepo = notificareRepo;
        this.participantRepo = participantRepo;
        this.postRepo = postRepo;
        this.voluntarRepo = voluntarRepo;
        this.userSessionRepo = userSessionRepo;
        this.sponsorTypeRepo = sponsorTypeRepo;
        this.profilePicRepo = profilePicRepo;
    }

    @Override
    public UserInfo login(String username, String password) throws ServiceException {
        password = RSA.decode(password, PRIVATE_KEY);
        password = saltAndHash(password);
        logger.trace("");
        logger.info("Login { "+username + " " + password+ " }");

        Utilizator u =voluntarRepo.findByCredentials(username, password);
        if(u!=null) {
            var token = userSessionRepo.createToken(u);
            logger.info("Ok:{}",token);
            logger.traceExit();
            return new UserInfo("Voluntar",u, token);
        }
        u =adminRepo.findByCredentials(username, password);
        if(u!=null) {
            var token = userSessionRepo.createToken(u);
            logger.info("Ok:{}",token);
            logger.traceExit();
            return new UserInfo("Admin",u, token);
        }
        logger.info("Error:{}", "invalid credentials");
        logger.traceExit();
        throw new ServiceException("Invalid username or password");
    }

    @Override
    public void logout(String token) {
        logger.trace("");
        logger.info("Logout{}", token);
        userSessionRepo.close(token);
        logger.info("Ok:{}", "Logout succesful");
        logger.traceExit();
    }

    @Override
    public Eveniment[] getActualEvenimente() {
        logger.trace("");
        logger.info("Get evenimente actuale ");

        List<Eveniment> eveniments = new ArrayList<>();

        for(Eveniment eveniment: evenimentRepo.getAll()){
            if(Objects.equals(eveniment.getStatus(), "PENDING")){
                eveniments.add(eveniment);
            }
        }
        logger.traceEntry();
        return eveniments.toArray(Eveniment[]::new);
    }

    @Override
    public Utilizator createAccount(String username, String password, String email, String nume, String prenume) throws ServiceException {
        password = RSA.decode(password, PRIVATE_KEY);
        logger.traceEntry("");
        logger.info("Create Account{}", username + " " + password + " " + email + " " + nume + " " + prenume);
        if(Objects.equals(username, "") || Objects.equals(password, "") || Objects.equals(email, "") || Objects.equals(nume, "") || Objects.equals(prenume, ""))
        {
            logger.info("Error{} ","Invalid inputs");
            logger.traceExit();
            throw new ServiceException("Invalid inputs for utilizator");
        }
        Voluntar voluntar = new Voluntar(username, saltAndHash(password), email, nume, prenume,0,false,new HashSet<>());
        voluntarRepo.add(voluntar);
        logger.info("Ok{} ", voluntar);
        logger.traceExit();
        return voluntar;
    }

    @Override
    public Voluntar getVoluntarById(Integer id) throws ServiceException {
        logger.trace("");
        logger.info("GetVoluntarById {} ", id);
        var vol=voluntarRepo.getById(id);
        if(vol==null){
            logger.info("Error:{}", " Voluntar found by id");
            logger.traceExit();
            throw new ServiceException("Invalid Voluntar Id");
        }
        logger.info("Ok:{}", vol);
        logger.traceExit();
        return vol;
    }

    @Override
    public Participant getParticipantById(Integer id) throws ServiceException {
        logger.traceEntry("");
        logger.info("getParticipantById{} ", id);
        var part = participantRepo.getById(id);
        if(part==null)
        {
            logger.info("Error{} ","Invalid id");
            logger.traceExit();
            throw new ServiceException("Invalid Participant Id");
        }
        logger.info("Ok{} ","Participant found " + part);
        logger.traceExit();
        return part;
    }

    @Override
    public Admin getAdminById(Integer id) throws ServiceException {
        logger.trace("");
        logger.info("GetAdminById {} ", id);
        var vol=adminRepo.getById(id);
        if(vol==null){
            logger.info("Error:{}", " Admin found by id");
            logger.traceExit();
            throw new ServiceException("Invalid Admin Id");
        }
        logger.info("Ok:{}", vol);
        logger.traceExit();
        return vol;
    }

    @Override
    public Eveniment getEvenimentById(Integer id) throws ServiceException {
        logger.trace("");
        logger.info("GetEvenimentId {} ", id);

        var evt = evenimentRepo.getById(id);

        if(evt==null){
            logger.info("Error:{}", "Invalid Eveniment Id");
            logger.traceExit();
            throw new ServiceException("Invalid Eveniment Id");
        }

        logger.info("Ok:{}", evt);
        logger.traceExit();

        return evt;
    }

    @Override
    public Iterable<Interest> getInterests() {
        logger.trace("");
        logger.info("getInterests {}");
        Iterable<Interest> interests = interestRepo.getAll();
        logger.info("Ok:{}", ((Collection<?>) interests).size());
        logger.traceExit();
        return interests;
    }

    @Override
    public Interest getInterestByName(String name) throws ServiceException {
        logger.trace("");
        logger.info("getInterestsByName {}", name);
        var interest = interestRepo.getByName(name);

        if(interest==null){
            logger.info("Error:{}", "Interest not found");
            logger.traceExit();
            throw new ServiceException("Interest not found");
        }

        logger.info("Ok:{}", interest);
        logger.traceExit();
        return interest;
    }

    @Override
    public void addVoluntarInterest(Voluntar voluntar, Interest interest) throws ServiceException{
        logger.trace("");
        logger.info("addVoluntarInterest {}", interest + " to " + voluntar);
        if(voluntar == null){
            logger.info("Error:{}", "Voluntar not found");
            logger.traceExit();
            throw new ServiceException("Voluntar not found");
        }
        if(interest == null){
            logger.info("Error:{}", "Interest not found");
            logger.traceExit();
            throw new ServiceException("Interest not found");
        }
        voluntar.getInterests().add(interest);
        voluntarRepo.update(voluntar);
        logger.info("Ok:{}", voluntar);
        logger.traceExit();
    }

    @Override
    public void removeVoluntarInterest(Voluntar voluntar, Interest interest) throws ServiceException {
        logger.trace("");
        logger.info("removeVoluntarInterest {}", interest + " from " + voluntar);
        if(voluntar == null){
            logger.info("Error:{}", "Voluntar not found");
            logger.traceExit();
            throw new ServiceException("Voluntar not found");
        }
        if(interest == null){
            logger.info("Error:{}", "Interest not found");
            logger.traceExit();
            throw new ServiceException("Interest not found");
        }
        voluntar.getInterests().removeIf(i-> Objects.equals(i.getName(), interest.getName()));
        voluntarRepo.update(voluntar);
        logger.info("Ok:{}", voluntar);
        logger.traceExit();
    }

    @Override
    public Eveniment addEvent(Eveniment e) throws ServiceException {
        logger.trace("");
        logger.info("addEvent {}", e);

        var initiator = e.getInitiator();
        var organizer = participantRepo.add(new Participant(initiator, true));
        e.getParticipants().add(organizer);
        var ev = evenimentRepo.add(e);
        if(ev==null){
            logger.info("Error:{}", "Could not add event!");
            logger.traceExit();
            throw new ServiceException("Could not add event");
        }
        logger.info("Ok:{}", ev);
        logger.traceExit();
        return ev;
    }

    private Participant addParticipant(Voluntar voluntar, Eveniment event, boolean isOrganizer) throws ServiceException {
        logger.trace("");
        logger.info("addParticipantt {}", voluntar + "to" + event.getId());
        if(voluntar == null){
            logger.info("Error:{}", "Voluntar not found");
            logger.traceExit();
            throw new ServiceException("Voluntar not found");
        }
        if(event == null){
            logger.info("Error:{}", "Event not found");
            logger.traceExit();
            throw new ServiceException("Event not found");
        }
        var p=participantRepo.add(new Participant(voluntar, isOrganizer));
        if(p==null){
            logger.info("Error:{}", "Could not add participant");
            logger.traceExit();
            throw new ServiceException("Could not add participant");
        }
        for (Participant participant : event.getParticipants()) {
            if (participant.getVoluntar().getId().equals(voluntar.getId())) {
                logger.info("Error:{}", "Participant already exists");
                logger.traceExit();
                throw new ServiceException("Participant already exists");
            }
        }
        System.out.println(p);
        event.getParticipants().add(p);
        evenimentRepo.update(event);
        logger.info("Ok:{}", p);
        logger.traceExit();
        return p;
    }

    @Override
    public Participant addVolunteer(Voluntar voluntar, Eveniment event) throws ServiceException {
        logger.trace("");
        logger.info("addVolunteer {}", voluntar + "to" + event.getId());
        if(voluntar == null){
            logger.info("Error:{}", "Voluntar not found");
            logger.traceExit();
            throw new ServiceException("Voluntar not found");
        }
        if (event == null) {
            logger.info("Error:{}", "Event not found");
            logger.traceExit();
            throw new ServiceException("Event not found");
        }
        for(Participant p:event.getParticipants()){
            if(p.getVoluntar().getId().equals(voluntar.getId())){
                logger.info("Error:{}", "Voluntar already in event");
                logger.traceExit();
                throw new ServiceException("Voluntar already in event");
            }
        }
        logger.traceExit();
        return addParticipant(voluntar, event, false);
    }

    @Override
    public Participant addOrganizer(Voluntar voluntar, Eveniment event) throws ServiceException {
        logger.trace("");
        logger.info("addOrganizer {}", voluntar + "to" + event.getId());
        if(voluntar == null){
            logger.info("Error:{}", "Voluntar not found");
            logger.traceExit();
            throw new ServiceException("Voluntar not found");
        }
        for(Participant p:event.getParticipants()){
            if(p.getVoluntar().getId().equals(voluntar.getId())){
                logger.info("Error:{}", "Voluntar already in event");
                logger.traceExit();
                throw new ServiceException("Voluntar already in event");
            }
        }
        logger.traceExit();
        return addParticipant(voluntar, event, true);
    }

    @Override
    public Participant[] getParticipants(Eveniment event) throws ServiceException{
        logger.trace("");
        logger.info("getParticipants {}", event);
        if(event == null){
            logger.info("Error:{}", "Event not found");
            logger.traceExit();
            throw new ServiceException("Event not found");
        }
        var voluntars = event.getParticipants().stream().toArray(Participant[]::new);
        logger.info("Ok{}", voluntars.length);
        logger.traceExit();
        return voluntars;
    }

    @Override
    public Participant[] getOrganizers(Eveniment event) {
        logger.traceEntry();
        logger.info("getOrganizers{} ", " from " + event.getId());
        var organizers = event.getParticipants().stream()
                .filter(Participant::isOrganizer)
                .toArray(Participant[]::new);
        logger.info("Ok{} ", organizers.length);
        logger.traceExit();
        return organizers;
    }

    @Override
    public Participant[] getVolunteers(Eveniment event) {
        logger.traceEntry("");
        logger.info("getVolunteers{} ", "from " + event.getId());
        var volunteers = event.getParticipants().stream()
                .filter(p->!p.isOrganizer())
                .toArray(Participant[]::new);
        logger.info("Ok{} ",  volunteers.length);
        logger.traceExit();
        return volunteers;
    }

    @Override
    public Eveniment updateEveniment(Eveniment eveniment) {
        logger.traceEntry("");
        logger.info("updateEveniment{} ", eveniment.getId());
        evenimentRepo.update(eveniment);
        logger.info("Ok{} ");
        logger.traceExit();
        return eveniment;
    }

    @Override
    public Eveniment deleteParticipantFromEveniment(Participant participant, Eveniment eveniment) {
        logger.traceEntry("");
        logger.info("removeParticipant{}", "from " + eveniment.getId()+" total:"+ ((Collection<?>)eveniment.getParticipants()).size());
        eveniment.getParticipants().removeIf(p-> Objects.equals(p.getId(), participant.getId()));
        evenimentRepo.update(eveniment);
        logger.info("Participant sters {} ramasi :", ((Collection<?>)eveniment.getParticipants()).size());
        logger.traceExit();
        return eveniment;
    }

    @Override
    public UserSession getUserSession(String token) throws ServiceException {
        logger.traceEntry("");
        logger.info("GetUserSession{}");
        var session = userSessionRepo.findByToken(token);
        if(session==null){
            logger.info("Error{] ", "Invalid token");
            logger.traceExit();
            throw new ServiceException("Invalid user token");
        }
        logger.info("Ok{} ", "Successful");
        logger.traceExit();
        return session;
    }

    @Override
    public Eveniment[] getOrderedEveniments(EventOrderOption orderOption, int page, int itemsPerPage) {
        logger.traceEntry("");
        logger.info("getOrderedEveniments{} ", orderOption + " page: " + page + " itmes/page: " + itemsPerPage);
        Eveniment[] eveniments = evenimentRepo.getOrderedPaged(orderOption, page,itemsPerPage);
        logger.info("Ok{} ", eveniments.length);
        logger.traceExit();
        return eveniments;
    }

    @Override
    public Eveniment[] getByInterestOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage, String interest){
        logger.traceEntry("");
        logger.info("getOrderedEveniments{} ", orderOption + " page: " + page + " itmes/page: " + itemsPerPage);
        Eveniment[] eveniments = evenimentRepo.getByInterestOrderedPaged(orderOption, page,itemsPerPage, interest);
        logger.info("Ok{} ", eveniments.length);
        logger.traceExit();
        return eveniments;
    }

    @Override
    public Eveniment[] getEvenimentByOrganizerId(Integer initiatorId){
        logger.traceEntry();
        logger.info("getEvenimentByOrganizerId{} ", initiatorId);
        Eveniment[] eveniments = evenimentRepo.getByOrganizer(initiatorId);
        logger.info("Ok{} ", eveniments.length);
        logger.traceExit();
        return eveniments;
    }

    @Override
    public Eveniment[] getEvenimentByVoluntarId(Integer volId){
        logger.traceEntry();
        logger.info("getEvenimentByVoluntarId{} ", volId);
        Eveniment[] eveniments = evenimentRepo.getByVoluntar(volId);
        logger.info("Ok{} ", eveniments.length);
        logger.traceExit();
        return eveniments;
    }

    @Override
    public Post addPost(Post post) throws ServiceException {
        logger.trace("");
        logger.info("addPostare {}", post);
        if(post == null){
            logger.info("Error:{}", "Post not found");
            logger.traceExit();
            throw new ServiceException("Post not found");
        }
        logger.traceExit();
        return postRepo.add(post);
    }

    @Override
    public Iterable<Interest> getVoluntarInterest(Integer volId) throws ServiceException {
        logger.trace("");
        logger.info("getInterest of voluntar:  {}", volId);
        Voluntar voluntar = this.getVoluntarById(volId);
        Iterable<Interest> interests = voluntar.getInterests();
        logger.info("found interests {}", ((Collection<?>) interests).size());
        logger.traceExit();
        return interests;
    }

    @Override
    public CerereSponsor applyForSponsorship(CerereSponsor cerereSponsor) {
        logger.trace("");
        logger.info("apply for Sponsorship - voluntar: {} ", cerereSponsor.getVolunteer());
        CerereSponsor cerereSponsor1 = this.cerereSponsorRepo.applyforSponsorship(cerereSponsor);
        logger.info("application succesfull {}", cerereSponsor1.getVolunteer() + " " + cerereSponsor1.getStatus());
        logger.traceExit();
        return cerereSponsor1;
    }

    @Override
    public SponsorType getSponsorTypeByName(String name) throws ServiceException {
        logger.trace("");
        logger.info("getSponsorTypeByName {}", name);
        var sponsorType = sponsorTypeRepo.getByName(name);

        if(sponsorType==null){
            logger.info("Error:{}", "Sponsor Type not found");
            logger.traceExit();
            throw new ServiceException("Sponsor Type not found");
        }

        logger.info("Ok:{}", sponsorType);
        logger.traceExit();
        return sponsorType;
    }

    @Override
    public Iterable<SponsorType> getSponsorTypes() {
        logger.trace("");
        logger.info("getSponsorTypes {}");
        Iterable<SponsorType> sponsorTypes = sponsorTypeRepo.getAll();
        logger.info("Ok:{}", ((Collection<?>) sponsorTypes).size());
        logger.traceExit();
        return sponsorTypes;
    }

    @Override
    public CerereSponsor[] getPendingSponsorRequests() {
        logger.trace("");
        logger.info("getPendingSponsorRequests {}");
        Iterable<CerereSponsor> cerereSponsors = cerereSponsorRepo.getAll();
        List<CerereSponsor> cerereSponsorspending = new ArrayList<>();
        for(CerereSponsor cerereSponsor: cerereSponsors){
            if(cerereSponsor.getStatus().equals("pending")){
                cerereSponsorspending.add(cerereSponsor);
            }
        }
        logger.info("Ok:{}", ((Collection<?>) cerereSponsors).size());
        logger.traceExit();
        return  cerereSponsorspending.toArray(CerereSponsor[]::new);
    }

    @Override
    public CerereSponsor addCerereSponsor(CerereSponsor cerereSponsor) {
        logger.trace("");
        logger.info("addCerereSponsor {0}", cerereSponsor);
        CerereSponsor cerereSponsor1 = cerereSponsorRepo.add(cerereSponsor);
        logger.info("Ok:{}", cerereSponsor1);
        logger.traceExit();
        return cerereSponsor1;
    }

    @Override
    public CerereSponsor updateCerereSponsor(CerereSponsor cerereSponsor) {
        logger.trace("");
        logger.info("updateCerereSponsor {0}", cerereSponsor);
        CerereSponsor cerereSponsor1 = cerereSponsorRepo.update(cerereSponsor);
        logger.info("Ok:{}", cerereSponsor1);
        logger.traceExit();
        return cerereSponsor1;
    }

    @Override
    public CerereSponsor getCerereSponsorById(Integer id) {
        logger.trace("");
        logger.info("getCerereSponsorById {}", id);
        CerereSponsor cerereSponsor = cerereSponsorRepo.getById(id);
        logger.info("Ok:{}", cerereSponsor);
        logger.traceExit();
        return cerereSponsor;
    }

    private static String salt(String p){ return p+"_salted"+p.length(); }
    private static String saltAndHash(String p){
        return SHA256.hash(salt(p));
    }

    private static final String PRIVATE_KEY =
            "-----BEGIN PRIVATE KEY-----\n" +
                    "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGAa1B+kVIkWffxN8Q6\n" +
                    "xqRzJ08JzzlGv6CNNuPpa7foG0vGNeEMXDLQEK8OTJ1xYEz0MTmngpQx4OQ2Hb9Q\n" +
                    "1hs1Zphlc4ouEHG14gWNrX6920Z8xu9lNybiCtGkhAwUxSCjNrxz1Qr+26QbihcS\n" +
                    "McXgC7e3/5/YhiUr4lyCQAI6f0kCAwEAAQKBgBjlyweiPCbXfJKIp25Q1xqmnssC\n" +
                    "KeTptfmnNQ+10lcK5Ii5lumJLHbCdpnV6WkDUaBeFPwZr9zSda+/JF0YYPIHG9hV\n" +
                    "8U884k2UaDZbOy8RKu0/dOOkLSkAUYhh55Ss0EXULBnvxq41EuTpEyWYu+3hmJRK\n" +
                    "nJCH1CUWXpwlObGBAkEAy6ETtmmrEWxECroTMAuyx/8PG0gal+4CKP0F4jRAWe9c\n" +
                    "0p6nQAaZ3A1btPjV7/E3oYBBh9m8OyG8kOkMN07PEQJBAIbqEFy4xpZS5zCoFY95\n" +
                    "VGovsMAxmZo8soxtXHdcCe4RO5vHkraanLya1YmDMuzBI/erIBzuSVf0R0H7P/OR\n" +
                    "HLkCQQCSC6kzv33uNRRoDSUN5JYJUynmi0Rni1EJTNAXeRpeZorQlPGnvhRD+2C2\n" +
                    "33GxcfRQZMibQtL6Jiw0UrFsSZ3BAkA0rW2oFomLpmEYsXiBpbkdIPPdh0BXZb29\n" +
                    "cPH6tNg3uUjSAXG6lNIAHmCkKbMXmC4oBQwr36qJihrMm4KT4qQZAkAzm4md7f3O\n" +
                    "Nn+oAAdd7iaOXmFTBimpvETK8q2MvVIFGIZlHhVAsT5XLdL/e8hdKD7nhUZgJ237\n" +
                    "aZzLq0w6Thnc\n" +
                    "-----END PRIVATE KEY-----";

    private static final String RSA_PRIVATE_KEY =
            "-----BEGIN RSA PRIVATE KEY-----\n" +
                    "MIICWwIBAAKBgGtQfpFSJFn38TfEOsakcydPCc85Rr+gjTbj6Wu36BtLxjXhDFwy\n" +
                    "0BCvDkydcWBM9DE5p4KUMeDkNh2/UNYbNWaYZXOKLhBxteIFja1+vdtGfMbvZTcm\n" +
                    "4grRpIQMFMUgoza8c9UK/tukG4oXEjHF4Au3t/+f2IYlK+JcgkACOn9JAgMBAAEC\n" +
                    "gYAY5csHojwm13ySiKduUNcapp7LAink6bX5pzUPtdJXCuSIuZbpiSx2wnaZ1elp\n" +
                    "A1GgXhT8Ga/c0nWvvyRdGGDyBxvYVfFPPOJNlGg2WzsvESrtP3TjpC0pAFGIYeeU\n" +
                    "rNBF1CwZ78auNRLk6RMlmLvt4ZiUSpyQh9QlFl6cJTmxgQJBAMuhE7ZpqxFsRAq6\n" +
                    "EzALssf/DxtIGpfuAij9BeI0QFnvXNKep0AGmdwNW7T41e/xN6GAQYfZvDshvJDp\n" +
                    "DDdOzxECQQCG6hBcuMaWUucwqBWPeVRqL7DAMZmaPLKMbVx3XAnuETubx5K2mpy8\n" +
                    "mtWJgzLswSP3qyAc7klX9EdB+z/zkRy5AkEAkgupM7997jUUaA0lDeSWCVMp5otE\n" +
                    "Z4tRCUzQF3kaXmaK0JTxp74UQ/tgtt9xsXH0UGTIm0LS+iYsNFKxbEmdwQJANK1t\n" +
                    "qBaJi6ZhGLF4gaW5HSDz3YdAV2W9vXDx+rTYN7lI0gFxupTSAB5gpCmzF5guKAUM\n" +
                    "K9+qiYoazJuCk+KkGQJAM5uJne39zjZ/qAAHXe4mjl5hUwYpqbxEyvKtjL1SBRiG\n" +
                    "ZR4VQLE+Vy3S/3vIXSg+54VGYCdt+2mcy6tMOk4Z3A==\n" +
                    "-----END RSA PRIVATE KEY-----";
    @Override
    public List<Post> getPostsOfVoluntar(Integer volId) {
        logger.trace("");
        logger.info("getPostsOfVoluntar{} ", volId);
        List<Post> volPosts = postRepo.getPostsOfVol(volId);
//        for(Post p: postRepo.getAll()){
//            for(Eveniment ev: getEvenimentByVoluntarId(volId))
//                if(p.getEveniment().equals(ev.getId()))
//                    volPosts.add(p);
//        }
        logger.info("Ok:{}", volPosts.size());
        logger.traceExit();
        return volPosts;
    }

    @Override
    public List<Post> getAllPosts() {
        logger.trace("");
        logger.info("getAllPosts{} ");
        List<Post> volPosts = StreamSupport.stream(postRepo.getAll().spliterator(), false
        ).toList();
        logger.info("Ok:{}", volPosts.size());
        logger.traceExit();
        return volPosts;
    }

    @Override
    public byte[] getProfilePic(int userId) throws ServiceException {
        try {
            return profilePicRepo.getImage(userId);
        } catch (IOException e) {
            throw new ServiceException(e.getMessage());
        }
    }

    @Override
    public void setProfilePic(int userId, byte[] bytes) throws ServiceException {
        try {
            profilePicRepo.add(userId, bytes);
        } catch (IOException e) {
            throw new ServiceException(e.getMessage());
        }
    }

    @Override
    public void modifyExpPoints(Voluntar vol, Integer amount) {
        logger.trace("");
        logger.info("modifyExp Points of{} ", vol);
        Integer pct = voluntarRepo.modifyPoints(vol, amount);
        logger.info("Ok; New Points: {}", pct);
        logger.traceExit();
    }

    @Override
    public Utilizator getUserByName(String username) throws ServiceException {
        logger.trace("");
        logger.info("getUserByName{} ", username);
        for(Utilizator u: voluntarRepo.getAll()){
            if(u.getUsername().equals(username))
                return u;

        }
        for(Utilizator a: adminRepo.getAll()){
            if(a.getUsername().equals(username))
                return a;
        }
        logger.info("Ok");
        logger.traceExit();
        throw new ServiceException("User not found");

    }
    @Override
    public UserInfo resetPassword(String username, String password) {
        logger.trace("");
        logger.info("resetPassword{} ", username);
        String newpassword = saltAndHash(password);
        System.out.println(newpassword);
        System.out.println(password);
        for(Utilizator u: voluntarRepo.getAll()){
            if(u.getUsername().equals(username))
                voluntarRepo.changePassword(u.getUsername(), newpassword);
        }
        for (Utilizator a: adminRepo.getAll()){
            if(a.getUsername().equals(username))
                adminRepo.changePassword(a.getUsername(), newpassword);
        }
        logger.info("Ok");
        logger.traceExit();
        return null;
    }

    @Override
    public List<Post> getNewestPosts() {
        logger.trace("");
        logger.info("getNewestPosts{} ");
        return postRepo.getNewestPosts();
    }

    //sa nu te mai poti inscrie la un eveniment daca te-ai inscris deja

}