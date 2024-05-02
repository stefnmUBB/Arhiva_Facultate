package com.helpinghands.rest_services;

import com.helpinghands.domain.*;
import com.helpinghands.repo.data.EventOrderOption;
import com.helpinghands.rest_services.data.*;
import com.helpinghands.rest_services.dto.*;
import com.helpinghands.service.data.UserInfo;
import com.helpinghands.service.IService;
import com.helpinghands.service.ServiceException;
import com.helpinghands.service.security.RSA;
import org.apache.commons.io.IOUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@CrossOrigin
@RestController
@RequestMapping("/helpinghands")
public class HelpingHandsRestController {
    @Autowired
    private final IService service;

    public HelpingHandsRestController(IService service) {
        this.service = service;

        final String RSA_PUBLIC_KEY =
                "-----BEGIN PUBLIC KEY-----\n" +
                        "MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgGtQfpFSJFn38TfEOsakcydPCc85\n" +
                        "Rr+gjTbj6Wu36BtLxjXhDFwy0BCvDkydcWBM9DE5p4KUMeDkNh2/UNYbNWaYZXOK\n" +
                        "LhBxteIFja1+vdtGfMbvZTcm4grRpIQMFMUgoza8c9UK/tukG4oXEjHF4Au3t/+f\n" +
                        "2IYlK+JcgkACOn9JAgMBAAE=\n" +
                        "-----END PUBLIC KEY-----";

        final String RSA_PRIVATE_KEY =
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

        var cr = RSA.encode("Ana are mere", RSA_PUBLIC_KEY);
        System.out.println(cr);
        var dc = RSA.decode(cr, RSA_PRIVATE_KEY);
        System.out.println(dc);
    }

    @RequestMapping(value="/interests",method= RequestMethod.GET)
    public Interest[] getInterest(@RequestParam Optional<String> name, @RequestParam Optional<Integer> voluntar) throws ServiceException {
        if(name.isPresent())
            return Collections.singletonList(service.getInterestByName(name.get())).toArray(Interest[]::new);
        if(voluntar.isPresent())
            return StreamSupport.stream(service.getVoluntarInterest(voluntar.get()).spliterator(),false)
                    .toArray(Interest[]::new);
        return StreamSupport.stream(service.getInterests().spliterator(),false)
                .toArray(Interest[]::new);
    }

    @RequestMapping(value="/login",method = RequestMethod.POST)
    public UserInfo login(@RequestBody Credentials credentials) throws ServiceException {
        return service.login(credentials.getUsername(), credentials.getPassword());
    }

    @RequestMapping(value="/register",method = RequestMethod.POST)
    public ResponseEntity<?> UtilizatorRegister (@RequestBody UserRegisterParams params){
        try{
            Utilizator utilizator  =service.createAccount(params.getUsername(),params.getPassword(),params.getEmail(),params.getNume(),params.getPrenume());
            return new ResponseEntity<>(UserDTO.fromUtilizator(utilizator),HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(e.getMessage(),HttpStatus.BAD_REQUEST);
        }
    }

    @RequestMapping(value="/logout",method = RequestMethod.GET)
    public ResponseEntity<?> logout(@RequestParam String token){
        service.logout(token);
        return new ResponseEntity<String>("Ok",HttpStatus.OK);
    }

    @ExceptionHandler(Exception.class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    public String serviceException(Exception e) {
        System.out.println("Exception");
        e.printStackTrace();
        System.out.println(e.getMessage());
        return e.getMessage();
    }

    @ExceptionHandler(ServiceException.class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    public String serviceException(ServiceException e) {
        System.out.println("Service exc");
        e.printStackTrace();
        System.out.println(e.getMessage());
        return e.getMessage();
    }

    @ExceptionHandler(HHServerException.class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    public String serverException(HHServerException e) {
        System.out.println("HHServer exc");
        e.printStackTrace();
        return e.getMessage();
    }

    @RequestMapping(value="/evenimente", method = RequestMethod.POST)
    public EvenimentDTO addEveniment(@RequestBody EventParams eventParams) throws ServiceException, HHServerException {
        var token = eventParams.getToken();
        var session = service.getUserSession(token);
        if(!Objects.equals(session.getType(), "Voluntar")){
            throw new HHServerException("Doar voluntarii pot adauga evenimente");
        }

        var evdto = eventParams.getEveniment();
        var voluntar = (Voluntar)session.getUtilizator();
        var ev=new Eveniment(evdto.getName(), evdto.getDescription(), evdto.getStartDate(),
                evdto.getEndDate(), evdto.getLocation(), voluntar,
                evdto.getStatus());
        for(var interestName : evdto.getInterests()){
            var interest = service.getInterestByName(interestName);
            ev.getInterests().add(interest);
        }
        service.modifyExpPoints(ev.getInitiator(), 150);
        return EvenimentDTO.fromEveniment(service.addEvent(ev));
    }

    @RequestMapping(value="/evenimente", method = RequestMethod.GET)
    public EvenimentDTO[] getEvenimente(@RequestParam Optional<Integer> volId,
                                        @RequestParam Optional<Integer> isOrganizer,
                                        @RequestParam Optional<String> filter, @RequestParam Optional<Integer> page, @RequestParam Optional<Integer> perPage) throws ServiceException, HHServerException {
        if(volId.isEmpty()) {
            var _filter = EventOrderOption.valueOf(filter.orElse("NONE").toUpperCase());
            var _page = page.orElse(0);
            var _perPage = perPage.orElse(10);
            return Arrays
                    .stream(service.getOrderedEveniments(_filter, _page, _perPage))
                    .map(EvenimentDTO::fromEveniment)
                    .toArray(EvenimentDTO[]::new);
        }
        if(isOrganizer.isEmpty() || isOrganizer.get()==0) {
            return Arrays
                    .stream(service.getEvenimentByVoluntarId(volId.get()))
                    .map(EvenimentDTO::fromEveniment)
                    .toArray(EvenimentDTO[]::new);
        }
        else{
            return Arrays
                    .stream(service.getEvenimentByOrganizerId(volId.get()))
                    .map(EvenimentDTO::fromEveniment)
                    .toArray(EvenimentDTO[]::new);
        }
    }

    @RequestMapping(value = "/evenimente/{id_eveniment}/participants/{id_participant}", method = RequestMethod.DELETE)
    public ResponseEntity<?> removeVoluntarFromEveniment(@PathVariable Integer id_eveniment, @PathVariable Integer id_participant, @RequestParam String token){
        try{
            service.getUserSession(token); // check if valid token
            System.out.println("here");
            Participant voluntar = service.getParticipantById(id_participant);
            Eveniment eveniment = service.getEvenimentById(id_eveniment);

            var organizers = service.getOrganizers(eveniment);

            if(organizers.length==1 && Objects.equals(organizers[0].getId(), voluntar.getId())) {
                throw new HHServerException("Can't remove the only organizer of the event");
            }

            Eveniment eveniment_final = service.deleteParticipantFromEveniment(voluntar,eveniment);
            service.modifyExpPoints(service.getVoluntarById(voluntar.getVoluntar().getId()), -100);
            return new ResponseEntity<EvenimentDTO>(EvenimentDTO.fromEveniment(eveniment_final),HttpStatus.OK);
        }catch (Exception e){
            System.out.println(e.getMessage());
            return new ResponseEntity<String>(e.getMessage(),HttpStatus.BAD_REQUEST);
        }
    }

    @RequestMapping(value = "/evenimente/{id_eveniment}/participants", method = RequestMethod.GET)
    public ParticipantDTO[] getEvenimentParticipants(@PathVariable Integer id_eveniment) throws ServiceException {
        var ev = service.getEvenimentById(id_eveniment);
        return Arrays.stream(service.getParticipants(ev))
                .map(ParticipantDTO::fromParticipant)
                .toArray(ParticipantDTO[]::new);
    }

    @RequestMapping(value = "/evenimente/{id_eveniment}/participants", method = RequestMethod.PUT)
    public ResponseEntity<?> addVoluntarToEveniment(@PathVariable Integer id_eveniment, @RequestBody AddVoluntarRequestData reqData){
        // JSON Body :  {"idVoluntar":"42", "role":"organizer", "token":"..."}
        try{
            var userSession = service.getUserSession(reqData.getToken());

            if(Objects.equals(reqData.getRole(), "organizer") && !Objects.equals(userSession.getType(), "Admin")){
                throw new HHServerException("Invalid permission to add organizer");
            }
            if(Objects.equals(reqData.getRole(), "volunteer") && reqData.getIdVoluntar()!=userSession.getUtilizator().getId()){
                throw new HHServerException("Volunteers can only self-add to eveniment");
            }

            Eveniment eveniment = service.getEvenimentById(id_eveniment);
            Voluntar voluntar = service.getVoluntarById(reqData.getIdVoluntar());
            Participant participant;
            System.out.println(reqData.getRole());
            if(Objects.equals(reqData.getRole(), "organizer")){
                participant = service.addOrganizer(voluntar, eveniment);
            }
            else if(Objects.equals(reqData.getRole(), "volunteer")){
                participant = service.addVolunteer(voluntar, eveniment);
            }
            else
                return new ResponseEntity<>("Invalid role", HttpStatus.BAD_REQUEST);
            service.modifyExpPoints(service.getVoluntarById(reqData.getIdVoluntar()), 100);
            return new ResponseEntity<>(ParticipantDTO.fromParticipant(participant)
                    ,HttpStatus.OK);
        }catch (Exception e){
            System.out.println(e.getMessage());
            return new ResponseEntity<>(e.getMessage(),HttpStatus.BAD_REQUEST);}
    }

    @RequestMapping(value="/volunteers/{id}", method=RequestMethod.GET)
    public VoluntarDTO getUserXP(@PathVariable int id) throws ServiceException {
        var user = service.getVoluntarById(id);
        return VoluntarDTO.fromVoluntar(user);
    }

    @RequestMapping(value = "/evenimente/{id}", method = RequestMethod.PUT)
    public EvenimentDTO update(@PathVariable Integer id, @RequestBody EventParams eventParams) throws ServiceException, HHServerException {
        var token = eventParams.getToken();
        var session = service.getUserSession(token);
        if(!Objects.equals(session.getType(), "Voluntar")){
            throw new HHServerException("Doar voluntarii pot modifica evenimente");
        }
        var evdto = eventParams.getEveniment();
        var evt = service.getEvenimentById(id);

        var voluntar = (Voluntar)session.getUtilizator();
        if(Arrays.stream(service.getOrganizers(evt))
                .map(Participant::getVoluntar)
                .noneMatch(o-> Objects.equals(o.getId(), voluntar.getId()))){
            throw new HHServerException("Permission denied. User is not organizer for target event");
        }

        if(evdto.getName()!=null)
            evt.setName(evdto.getName());
        if(evdto.getDescription()!=null)
            evt.setDescription(evdto.getDescription());
        if(evdto.getStartDate()!=null)
            evt.setStartDate(evdto.getStartDate());
        if(evdto.getEndDate()!=null)
            evt.setEndDate(evdto.getEndDate());
        if(evdto.getInterests()!=null) {
            evt.setInterests(Arrays.stream(evdto.getInterests())
                    .map(name -> {
                        try {
                            return service.getInterestByName(name);
                        } catch (ServiceException e) {
                            throw new RuntimeException(e);
                        }
                    })
                    .collect(Collectors.toSet()));
        }

        evt.setStatus(evdto.getStatus());

        return EvenimentDTO.fromEveniment(service.updateEveniment(evt));
    }

    @RequestMapping(value="/posts", method = RequestMethod.POST)
    public ResponseEntity<?> addPost (@RequestBody PostDTO postDTO){
        try{
            Post post = new Post(postDTO.getDescriere(),LocalDateTime.now(),service.getEvenimentById(postDTO.getIdEv()), service.getAdminById(postDTO.getIdUser()));
            Post p = service.addPost(post);
            return new ResponseEntity<Post>(p,HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<String>(e.getMessage(),HttpStatus.BAD_REQUEST);
        }
    }
    @RequestMapping(value = "/interests/{id}", method = RequestMethod.GET)
    public Iterable<Interest> getInterests(@PathVariable Integer id) throws ServiceException {
        return service.getVoluntarInterest(id);
    }
    @RequestMapping(value = "/sponsorship", method = RequestMethod.POST)
    public ResponseEntity<?> applyForSponsorship(@RequestBody CerereDTO cerereDTO){
        try{
            CerereSponsor cerereSponsor = new CerereSponsor(service.getVoluntarById(cerereDTO.getVolId()), cerereDTO.getCifFirma(), cerereDTO.getTelefon(), cerereDTO.getAdresa(), cerereDTO.getNumeFirma(),service.getSponsorTypeByName(cerereDTO.getSponsorType()), "pending");
            CerereSponsor cerereSp = service.applyForSponsorship(cerereSponsor);
            service.modifyExpPoints(cerereSp.getVolunteer(),50);
            return new ResponseEntity<CerereDTO>(cerereDTO, HttpStatus.OK);
        } catch (ServiceException e) {
            throw new RuntimeException(e);
        } catch (Exception e){
            return new ResponseEntity<String>(e.getMessage(),HttpStatus.BAD_REQUEST);
        }
    }
    @RequestMapping(value = "/sponsorTypes", method = RequestMethod.GET)
    public Iterable<SponsorType> getSponsorTypes(){
        return service.getSponsorTypes();
    }


    @RequestMapping(value = "/cererisponsoripending",method = RequestMethod.GET)
    public CerereSponsor[] getCereriSponsorPending(){
        return service.getPendingSponsorRequests();
    }

   @RequestMapping(value = "/cererisponsors",method= RequestMethod.POST)
    public ResponseEntity<?> addCerereSponsorizare(@RequestBody CerereDTO cerereDTO){
       try{
           CerereSponsor cerereSponsor = new CerereSponsor(service.getVoluntarById(cerereDTO.getVolId()), cerereDTO.getCifFirma(), cerereDTO.getTelefon(), cerereDTO.getAdresa(), cerereDTO.getNumeFirma(),service.getSponsorTypeByName(cerereDTO.getSponsorType()), "pending");
           CerereSponsor cerereSponsor1 = service.addCerereSponsor(cerereSponsor);
           return new ResponseEntity<CerereDTO>(cerereDTO,HttpStatus.OK);
       } catch (Exception e) {
           return new ResponseEntity<String>(e.getMessage(),HttpStatus.BAD_REQUEST);
       }
   }

   @RequestMapping(value = "/cererisponsors/{id}",method = RequestMethod.PUT)
    public ResponseEntity<?> updateCerereSponsorizare(@PathVariable Integer id, @RequestBody CerereDTO cerereDTO){

         try {
             CerereSponsor cerereSponsor = service.getCerereSponsorById(id);
             cerereSponsor.setAdresaSediului(cerereDTO.getAdresa());
             cerereSponsor.setNumeFirma(cerereDTO.getNumeFirma());
             cerereSponsor.setCifFirma(cerereDTO.getCifFirma());
             cerereSponsor.setTelefon(cerereDTO.getTelefon());
             cerereSponsor.setStatus(cerereDTO.getStatus());
             System.out.println(service.getSponsorTypeByName(cerereDTO.getSponsorType()));
             cerereSponsor.setSponsorType(service.getSponsorTypeByName(cerereDTO.getSponsorType()));
             System.out.println(cerereDTO.getVolId());
             cerereSponsor.setVolunteer(service.getVoluntarById(cerereDTO.getVolId()));

             CerereSponsor cerereSponsor1 = service.updateCerereSponsor(cerereSponsor);
             CerereDTO cerereDTO1 = new CerereDTO(cerereSponsor1.getId(),cerereSponsor1.getVolunteer().getId(),cerereSponsor1.getCifFirma(),cerereSponsor1.getSponsorType().getName(),cerereSponsor1.getTelefon(),cerereSponsor1.getAdresaSediului(),cerereSponsor1.getNumeFirma(),cerereSponsor1.getStatus());
             return new ResponseEntity<CerereDTO>(cerereDTO1, HttpStatus.OK);
         }catch (Exception e){
             return new ResponseEntity<String>(e.getMessage(),HttpStatus.BAD_REQUEST);
         }
   }

   @RequestMapping(value="/cereresponsor/{id}", method = RequestMethod.GET)
    public ResponseEntity<?> getCerereSponsorById(@PathVariable Integer id) {
       System.out.println("am ajuns aici nu e bad rewuest");
       try {
           CerereSponsor cerereSponsor = service.getCerereSponsorById(id);
           return new ResponseEntity<CerereSponsor>(cerereSponsor, HttpStatus.OK);
       } catch (Exception e) {
           return new ResponseEntity<String>(e.getMessage(), HttpStatus.BAD_REQUEST);
       }
   }


    @RequestMapping(value = "/actualevenimente", method = RequestMethod.GET)
    public Eveniment[] getActualEvenimente() {
        return service.getActualEvenimente();
    }


    @RequestMapping(value="/posts",method = RequestMethod.GET)
    public List<PostVolDTO> getAllPosts(){
        List<PostVolDTO> postVolDTOSList = new ArrayList<PostVolDTO>();
        for(Post p: service.getNewestPosts())
        {
            PostVolDTO postVolDTO = new PostVolDTO(p.getId(), p.getAuthor().getId(),p.getAuthor().getNume()+" "+p.getAuthor().getPrenume(), EvenimentNoParticipantsDTO.fromEveniment(p.getEveniment()), p.getDescriere(), p.getData());
            postVolDTOSList.add(postVolDTO);
        }
        return  postVolDTOSList;
    }

    @RequestMapping(value="/posts/{id}",method = RequestMethod.GET)
    public List<PostVolDTO> getPostOfVol(@PathVariable Integer id){
        List<PostVolDTO> postVolDTOSList = new ArrayList<PostVolDTO>();
        for(Post p: service.getPostsOfVoluntar(id))
        {
            PostVolDTO postVolDTO = new PostVolDTO(p.getId(), p.getAuthor().getId(),p.getAuthor().getNume()+" "+p.getAuthor().getPrenume(), EvenimentNoParticipantsDTO.fromEveniment(p.getEveniment()), p.getDescriere(), p.getData());
            postVolDTOSList.add(postVolDTO);
        }
        return  postVolDTOSList;
    }
    @RequestMapping(value="/users/{id}/pp",method = RequestMethod.GET,
            produces = MediaType.IMAGE_JPEG_VALUE)
    public @ResponseBody byte[] getProfilePic(@PathVariable int id) throws IOException, ServiceException {
        return service.getProfilePic(id);
    }

    @RequestMapping(value="/users/{id}/pp",method = RequestMethod.PUT,
            produces = MediaType.IMAGE_JPEG_VALUE)
    public String setProfilePic(@PathVariable int id, @RequestBody PPDTO pp) throws IOException, ServiceException, HHServerException {
        var userSession = service.getUserSession(pp.getToken());
        if(userSession.getUtilizator().getId()!=id)
            throw new HHServerException("Invalid permissions");
        service.setProfilePic(id, pp.getBytes());
        return "OK";
    }

    @RequestMapping(value="/users/passwordreset",method = RequestMethod.PUT)
    public String change(@RequestBody Credentials credentials) throws ServiceException {
        service.resetPassword(credentials.getUsername(), credentials.getPassword());
        return "OK";
    }


    @RequestMapping(value="/evenimentefilter", method = RequestMethod.GET)
    public EvenimentDTO[] getEvenimenteInterese(@RequestParam Optional<Integer> volId,
                                                @RequestParam Optional<Integer> isOrganizer,
                                                @RequestParam Optional<String> filter,
                                                @RequestParam Optional<Integer> page,
                                                @RequestParam Optional<Integer> perPage,
                                                @RequestParam Optional<String> interest) throws ServiceException, HHServerException {
        if(volId.isEmpty()) {
            var _filter = EventOrderOption.valueOf(filter.orElse("NONE").toUpperCase());
            var _page = page.orElse(0);
            var _perPage = perPage.orElse(10);
            return Arrays
                    .stream(service.getByInterestOrderedPaged(_filter, _page, _perPage, interest.get()))
                    .map(EvenimentDTO::fromEveniment)
                    .toArray(EvenimentDTO[]::new);
        }
        if(isOrganizer.isEmpty() || isOrganizer.get()==0) {
            return Arrays
                    .stream(service.getEvenimentByVoluntarId(volId.get()))
                    .map(EvenimentDTO::fromEveniment)
                    .filter(evenimentDTO -> Arrays.stream(evenimentDTO.getInterests()).anyMatch(
                            p -> Objects.equals(p,interest.get())
                    ))
                    .toArray(EvenimentDTO[]::new);
        }
        else{
            return Arrays
                    .stream(service.getEvenimentByOrganizerId(volId.get()))
                    .map(EvenimentDTO::fromEveniment)
                    .filter(evenimentDTO -> Arrays.stream(evenimentDTO.getInterests()).anyMatch(
                            p -> Objects.equals(p,interest.get())
                    ))
                    .toArray(EvenimentDTO[]::new);
        }
    }

}
