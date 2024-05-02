package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.domain.validators.IValidator;
import sn.socialnetwork.domain.validators.ValidationException;

import java.io.*;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;

public abstract class FileRepo<ID, E extends Entity<ID>> extends InMemoryRepo<ID,E> {
    String fileName;

    /**
     * Creates new file repository instance
     * @param fileName file name
     * @param validator entity validator
     */
    FileRepo(String fileName, IValidator<E> validator) {
        super(validator);
        this.fileName=fileName;
        load();
    }

    /**
     * adds new entity to the sn.socialnetwork.repo
     * @param entity entity to be added
     * @return added entity
     * @throws EntityAlreadyExistsException if the entity id is already found in the sn.socialnetwork.repo
     */
    public E add(E entity) throws EntityAlreadyExistsException {
        entity = super.add(entity);
        if(entity!=null) {
            saveAppend(entity);
        }
        return entity;
    }

    /**
     * updates entity by id
     * @param entity new entity with modified properties
     * @return modified entity
     */
    public E update(E entity){
        entity = super.update(entity);

        saveAll();

        return entity;
    }

    /**
     * remove entity from the sn.socialnetwork.repo
     * @param id Id of entity to delete
     * @return removed entity
     */
    public E remove(ID id){
        E entity = super.remove(id);
        if(entity!=null) {
            saveAll();
        }
        return entity;
    }

    /**
     * Creates new entity based on a list of text attributes
     * @param attributes list of attributes that are used to create the entity
     * @return resulted entity from the attributes
     */
    public abstract E extractEntity(List<String> attributes);

    /**
     * Converts entity to string to be saved in file
     * @param entity entity to save
     * @return save string
     */
    public abstract String entityAsString(E entity);

    /**
     * loads the sn.socialnetwork.repo data from file
     */
    private void load() {
        int linecnt = 0;
        try (BufferedReader br = new BufferedReader(new FileReader(fileName))) {
            String linie;
            while((linie=br.readLine())!=null) {
                linecnt++;
                if(Objects.equals(linie,"")) {
                    System.out.println("[DBG] Empty line");
                }
                List<String> attr= Arrays.asList(linie.split(";"));
                E e=extractEntity(attr);
                super.add(e);
            }
        } catch (FileNotFoundException e) {
            System.out.println("File not found "+fileName);
            System.exit(-4);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (EntityAlreadyExistsException e) {
            throw new RuntimeException(e);
        }
        catch(ArrayIndexOutOfBoundsException e){
            System.out.println("Incomplete entity in "+fileName+" at Line "+linecnt);
            System.out.println("File is possibly corrupt.");
            System.exit(-1);
        }
        catch(NumberFormatException e){
            System.out.println("Number parse error in "+fileName+" at Line "+linecnt);
            System.out.println("File is possibly corrupt.");
            System.exit(-2);
        }
        catch(ValidationException e){
            System.out.println("Failed to validate entity in "+fileName+" at Line "+linecnt);
            System.out.println(e.getMessage());
            System.out.println("File is possibly corrupt.");
            System.exit(-3);
        }
    }

    /**
     * saves an entity at the end of the file
     * @param entity entity to be saved
     */
    private void saveAppend(E entity) {
        try {
            File file = new File(fileName);
            BufferedWriter bW = new BufferedWriter(new FileWriter(fileName,true));
            if(!newLineExists(file)) {
                bW.newLine();
            }
            bW.write(entityAsString(entity));
            bW.newLine();
            bW.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * save all entities
     */
    private void saveAll() {
        try (BufferedWriter bW = new BufferedWriter(new FileWriter(fileName,false))) {
            for(var entity : getAll()) {
                bW.write(entityAsString(entity));
                bW.newLine();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    boolean newLineExists(File file) throws IOException {
        // https://stackoverflow.com/questions/28795440/check-if-a-new-line-exists-at-end-of-file
        RandomAccessFile fileHandler = new RandomAccessFile(file, "r");
        long fileLength = fileHandler.length() - 1;
        if (fileLength < 0) {
            fileHandler.close();
            return true;
        }
        fileHandler.seek(fileLength);
        byte readByte = fileHandler.readByte();
        fileHandler.close();

        if (readByte == 0xA || readByte == 0xD) {
            return true;
        }
        return false;
    }
}
