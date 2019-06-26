#include "FileServe.h"

// [VARIABELEN]
#define REQ_BUF_SZ 20 // Buffergrootte voor HTTP aanvragen.
File webFile;
char HTTP_req[REQ_BUF_SZ] = {
    0};             // gebufferde HTTP aanvraag opgeslagen als een 'null terminated' string.
char req_index = 0; // index naar HTTP_req buffer

EthernetServer fileServer(8080); // Intialiseer de ethernet server bibliotheek
                                 // met de poort om te gebruiken.

void FileServeSetup()
{
    Serial.print("Initializing SD card...");
    if (!SD.begin(4))
    {
        Serial.println("initialization failed!");
    }
    else
    {
        Serial.println("initialization done.");
    }
}
void FileServeStartServer()
{
    fileServer.begin(); // Start de server.
}
void FileServeLoop()
{
    EthernetClient client =
        fileServer.available(); // Luister naar aanvraag van clienten.
    if (client)
    {
        // Er is een nieuwe client aanvraag.
        Serial.println("[-- Nieuwe client --]");
        // Elke HTTP aanvraag eindigd in een lege lijn.
        boolean currentLineIsBlank = true;
        while (client.connected())
        {
            if (client.available())
            {                           // Er is client data aanwezig.
                char c = client.read(); // Lees 1 byte van de client data.
                // Buffer het eerste gedeelte van de HTTP aanvraag in de string
                // "HTTP_req array". Laat het laaste element in de array als 0 om de
                // string te beeindigen met "null" (REQ_BUF_SZ - 1)
                if (req_index < (REQ_BUF_SZ - 1))
                {
                    HTTP_req[req_index] =
                        c; // Bewaar de HTTP aanvraag karakters in een array.
                    req_index++;
                }
                Serial.write(c); // Print het karakter naar de seriele monitor.
                if (c == '\n' && currentLineIsBlank)
                {
                    auto st = String(HTTP_req);
                    st.remove(0, 5);
                    st.remove(8);
                    Serial.print("|");
                    Serial.print(st);
                    Serial.print("|");
                    Serial.println("");
                    webFile = SD.open(st.c_str(), FILE_READ);

                    if (webFile)
                    {
                        client.println("HTTP/1.1 200 OK");
                        client.println("Content-Type: text/yaml");
                        client.println("Connnection: close");
                        client.println();
                        while (webFile.available())
                        {
                            // Stuur de gegevens naar de client
                            client.write(webFile.read());
                        }
                        webFile.close();
                    }
                    else
                    {
                        client.println("HTTP/1.1 404 Not Found");
                        client.println("Connnection: close");
                        client.println();
                    }

                    // Reset buffer index en alle buffer elementen naar 0.
                    req_index = 0;
                    StrClear(HTTP_req, REQ_BUF_SZ);
                    break; // Dit is nodig om de connectie te beeindigen.
                }
                if (c == '\n')
                {
                    currentLineIsBlank = true;
                } // Laatste karakter ontvangen van de client, start een nieuwe lijn met
                  // volgende karakters.
                else if (c != '\r')
                {
                    currentLineIsBlank = false;
                } // Je hebt een nieuw karakter ontvangen van de client.
            }
        }
        delay(1);      // Geef de browser wat tijd om de data te ontvangen.
        client.stop(); // Sluit de connectie.
        Serial.println("[-- Client verbroken --]");
        Serial.println("");
    }
}

// Zet elk element van str naar 0 (maak de array leeg)
void StrClear(char *str, char length)
{
    for (int i = 0; i < length; i++)
    {
        str[i] = 0;
    }
}
