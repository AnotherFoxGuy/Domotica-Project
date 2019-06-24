#include "REST_Endpoints.h"


void setupRestEndpoints(aREST *rest){
    // Function to be exposed
    char l[4] = "led";
    rest->function(l, reinterpret_cast<int (*)(String)>(&ledControl));
}

// Custom function accessible by the API
int ledControl(const String& command) {

    // Get state from command
    int state = command.toInt();

    digitalWrite(6, state);
    return 1;
}