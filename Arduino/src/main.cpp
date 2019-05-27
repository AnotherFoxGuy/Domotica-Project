#include <PinChangeInterruptHandler.h>
#include <RFReceiver.h>

// Listen on digital pin 2
RFReceiver receiverTwo(2);

void setup() {
    Serial.begin(9600);
    receiverTwo.begin();
}

void loop() {
    char msg[MAX_PACKAGE_SIZE];
    byte senderId = 0;
    byte packageId = 0;
    byte len = receiverTwo.recvPackage((byte *) msg, &senderId, &packageId);

    Serial.println("");
    Serial.print("Package: ");
    Serial.println(packageId);
    Serial.print("Sender: ");
    Serial.println(senderId);
    Serial.print("Message: ");
    Serial.println(msg);
}