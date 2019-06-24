#include "aREST.h"

// Some specializations of our template
template<>
void aREST::addToBuffer(bool toAdd, bool quotable) {
    addStringToBuffer(toAdd ? "true" : "false", false);   // Booleans aren't quoted in JSON
}


template<>
void aREST::addToBuffer(const char *toAdd, bool quotable) {
    addStringToBuffer(toAdd, quotable);                       // Strings must be quoted
}


template<>
void aREST::addToBuffer(const String *toAdd, bool quotable) {
    addStringToBuffer(toAdd->c_str(), quotable);           // Strings must be quoted
}


template<>
void aREST::addToBuffer(const String toAdd, bool quotable) {
    addStringToBuffer(toAdd.c_str(), quotable);           // Strings must be quoted
}


template<>
void aREST::addToBuffer(char toAdd[], bool quotable) {
    addStringToBuffer(toAdd, quotable);           // Strings must be quoted
}