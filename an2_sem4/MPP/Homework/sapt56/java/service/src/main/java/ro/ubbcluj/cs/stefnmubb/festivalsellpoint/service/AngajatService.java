package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.IAngajatRepo;

public class AngajatService extends AbstractService<Integer, Angajat, IAngajatRepo>
    implements IAngajatService {
    public AngajatService(IAngajatRepo repo) {
        super(repo);
    }

    @Override
    public void register(Angajat angajat) throws EntityRepoException {
        var username = angajat.getUsername();
        var password = Utils.computeSha256Hash(angajat.getPassword());
        var email = angajat.getEmail();
        add(new Angajat(username, password, email));
    }

    @Override
    public Angajat login(String username, String password) throws EntityRepoException {
        return getRepo().findByCredentials(username, Utils.computeSha256Hash(password));
    }
}
