package sn.socialnetwork.domain;

public class User extends Entity<Long> {
    private String firstName;
    private String lastName;
    private String email;
    private String password;
    private Integer age;

    public User() { }

    /**
     * Creates a new User instance
     * @param firstName User first name
     * @param lastName User last name
     * @param email User email
     * @param password User password
     * @param age User age (14..100)
     *
     * @see sn.socialnetwork.domain.validators.NameValidator
     * @see sn.socialnetwork.domain.validators.EmailValidator
     */
    public User(String firstName, String lastName, String email, String password, Integer age) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.age = age;
    }

    /**
     * Creates text representation of the User instance
     * @return String obtained from User's properties
     */
    @Override
    public String toString() {
        return "User{" +
                "ID='" + getId() + '\'' +
                ", firstName='" + firstName + '\'' +
                ", lastName='" + lastName + '\'' +
                ", email='" + email + '\'' +
                ", password='" + password + '\'' +
                ", age=" + age +
                '}';
    }

    /**
     * Retrieves user first name
     * @return user first name
     */
    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    /**
     * Retreives user last name
     * @return user last name
     */
    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    /**
     * Retrieves user email
     * @return user email
     */
    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    /**
     * Retrieves user password (kinda...)
     * @return user password
     */
    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    /**
     * Retrieves age of the user
     * @return user age
     */
    public Integer getAge() {
        return age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public String getFullName() {
        return getLastName() + " " + getFirstName();
    }
}
