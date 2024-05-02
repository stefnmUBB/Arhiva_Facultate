package com.helpinghands.client_admin;

import com.helpinghands.client_admin.data.UserCredentials;
import com.helpinghands.domain.*;
import com.helpinghands.repo.data.EventOrderOption;
import com.helpinghands.rest_services.dto.CerereDTO;
import com.helpinghands.rest_services.dto.PostDTO;
import com.helpinghands.service.IService;
import com.helpinghands.service.ServiceException;
import com.helpinghands.service.data.UserInfo;
import com.helpinghands.service.security.RSA;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.ResourceAccessException;
import org.springframework.web.client.RestTemplate;

import java.util.List;
import java.util.concurrent.Callable;

public class ClientServiceImpl implements IService{
    public static final String URL = "http://localhost:8080/helpinghands";
    private RestTemplate restTemplate = new RestTemplate();
    private <T> T execute(Callable<T> callable) {
        try {
            return callable.call();
        } catch (ResourceAccessException | HttpClientErrorException e) { // server down, resource exception
            throw new RuntimeException(e.getMessage());
        } catch (Exception e) {
            throw new RuntimeException(e.getMessage());
        }
    }

    /*private String decrypt_password(String enc){
        Cipher encryptCipher = null;
        try {
            encryptCipher = Cipher.getInstance("RSA");
        } catch (NoSuchAlgorithmException | NoSuchPaddingException e) {
            throw new RuntimeException(e);
        }
    }*/

    @Override
    public UserInfo login(String username, String password) throws ServiceException {
        return execute(()->restTemplate.postForObject(URL+"/login",
                new UserCredentials(username, RSA.encode(password, PUBLIC_KEY))
                ,UserInfo.class));
    }

    @Override
    public void logout(String token) {

    }

    @Override
    public Eveniment[] getActualEvenimente() {
        return execute(()->restTemplate.getForObject(URL+"/actualevenimente",
                Eveniment[].class));
    }

    @Override
    public Utilizator createAccount(String username, String password, String email, String nume, String prenume) throws ServiceException {
        return null;
    }

    @Override
    public Iterable<Interest> getInterests() {
        return null;
    }

    @Override
    public Interest getInterestByName(String name) throws ServiceException {
        return null;
    }

    @Override
    public Voluntar getVoluntarById(Integer id) throws ServiceException {
        return null;
    }

    @Override
    public Participant getParticipantById(Integer id) throws ServiceException {
        return null;
    }

    @Override
    public Admin getAdminById(Integer id) throws ServiceException {
        return null;
    }

    @Override
    public Eveniment getEvenimentById(Integer id) throws ServiceException {
        return null;
    }

    @Override
    public void addVoluntarInterest(Voluntar voluntar, Interest interest) {

    }

    @Override
    public void removeVoluntarInterest(Voluntar voluntar, Interest interest) {

    }

    @Override
    public Eveniment addEvent(Eveniment e) throws ServiceException {
        return null;
    }

    @Override
    public Participant addVolunteer(Voluntar voluntar, Eveniment event) throws ServiceException {
        return null;
    }

    @Override
    public Participant addOrganizer(Voluntar voluntar, Eveniment event) throws ServiceException {
        return null;
    }

    @Override
    public Eveniment deleteParticipantFromEveniment(Participant participant, Eveniment eveniment) {
        return null;
    }

    @Override
    public UserSession getUserSession(String token) throws ServiceException {
        return null;
    }

    @Override
    public Eveniment[] getOrderedEveniments(EventOrderOption orderOption, int page, int itemsPerPage) {
        return new Eveniment[0];
    }

    @Override
    public Eveniment[] getByInterestOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage, String interest) {
        return new Eveniment[0];
    }

    @Override
    public Participant[] getParticipants(Eveniment event) {
        return new Participant[0];
    }

    @Override
    public Participant[] getOrganizers(Eveniment event) {
        return new Participant[0];
    }

    @Override
    public Participant[] getVolunteers(Eveniment event) {
        return new Participant[0];
    }

    @Override
    public Eveniment updateEveniment(Eveniment eveniment) {
        return null;
    }

    @Override
    public Eveniment[] getEvenimentByOrganizerId(Integer initiatorId) {
        return new Eveniment[0];
    }

    @Override
    public Eveniment[] getEvenimentByVoluntarId(Integer volId) {
        return new Eveniment[0];
    }

    @Override
    public Post addPost(Post post) {
        return execute(()->restTemplate.postForObject(URL+"/posts",
                new PostDTO(0,post.getDescriere(),post.getEveniment().getId(),post.getAuthor().getId(),post.getData())
                ,Post.class));
    }

    @Override
    public Iterable<Interest> getVoluntarInterest(Integer volId) throws ServiceException {
        return null;
    }

    @Override
    public CerereSponsor applyForSponsorship(CerereSponsor cerereSponsor) {
        return null;
    }

    @Override
    public SponsorType getSponsorTypeByName(String name) throws ServiceException {
        return null;
    }

    @Override
    public Iterable<SponsorType> getSponsorTypes() {
        return null;
    }

    @Override
    public CerereSponsor[] getPendingSponsorRequests() {
        return execute(()->restTemplate.getForObject(URL+"/cererisponsoripending",
                CerereSponsor[].class));
    }

    @Override
    public CerereSponsor addCerereSponsor(CerereSponsor cerereSponsor) {
        return null;
    }

    @Override
    public CerereSponsor updateCerereSponsor(CerereSponsor cerereSponsor) {
        CerereDTO cerereDTO = new CerereDTO(cerereSponsor);
        execute(()->{restTemplate.put(URL+"/cererisponsors/"+cerereDTO.getId(),cerereDTO);return 0;});
        return getCerereSponsorById(cerereSponsor.getId());

    }
    @Override
    public CerereSponsor getCerereSponsorById(Integer id) {
        return execute(()->restTemplate.getForObject(URL+"/cereresponsor/"+id,
                CerereSponsor.class));
    }

    @Override

    public List<Post> getPostsOfVoluntar(Integer volId) {
        return null;
    }

    @Override
    public List<Post> getAllPosts() {
        return null;
    }

    @Override
    public byte[] getProfilePic(int userId) throws ServiceException {
        return new byte[0];
    }

    @Override
    public void setProfilePic(int userId, byte[] bytes) throws ServiceException {

    }
    @Override
    public void modifyExpPoints(Voluntar vol, Integer amount) {
    }

    @Override

    public UserInfo resetPassword(String username, String password) {
        System.out.println(username);
        System.out.println(password);
        restTemplate.put(URL+"/users/passwordreset",new UserCredentials(username, password));
        return null;
    }

    @Override
    public Utilizator getUserByName(String username) throws ServiceException {
     return null;
    }
    public List<Post> getNewestPosts() {
        return null;
    }

    private static final String PUBLIC_KEY =
            "-----BEGIN PUBLIC KEY-----\n" +
            "MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgGtQfpFSJFn38TfEOsakcydPCc85\n" +
            "Rr+gjTbj6Wu36BtLxjXhDFwy0BCvDkydcWBM9DE5p4KUMeDkNh2/UNYbNWaYZXOK\n" +
            "LhBxteIFja1+vdtGfMbvZTcm4grRpIQMFMUgoza8c9UK/tukG4oXEjHF4Au3t/+f\n" +
            "2IYlK+JcgkACOn9JAgMBAAE=\n" +
            "-----END PUBLIC KEY-----";
}
