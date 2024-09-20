# Task

Develop a web based application for patients and doctors. The application is an online registry for patients who want to make an appointment with their local doctor.

## Patient use cases

- Log-in;
- Request an appointment with a doctor by providing specific data:
  - name;
  - Id of the doctor (existing in the system);
  - exact hour of the appointment;
- Receive confirmation for the appointment;
- Reschedule unapproved appointments;
- Patient is able to review his approved events;

## Doctor use cases

- Log-in;
- Review all requests for appointments;
- Approve or decline requests;
- Doctor is able to review his approved events;

## Note

Resolve potential conflicts with patients requesting the same hour for the same doctor simultaneously.