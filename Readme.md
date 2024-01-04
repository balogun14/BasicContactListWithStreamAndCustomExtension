### A simple BasicContactList that uses a txt file as the persistence layer
//!
class StreamReader : public BasicContactList {
    public:
    //! Constructor.  Opens the specified filename for reading and writing, creating it if
    //! necessary.
    explicit StreamReader(string filename);
    private:
    //! The name of the file we're using to persist data.
    string filename;
    };
    </s>
Date created: 04/01/24
