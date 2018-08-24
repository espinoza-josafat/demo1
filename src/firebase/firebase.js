import * as firebase from "firebase";

const config = {
  apiKey: "AIzaSyAeETXZpcbqAQv14RPkSfJ2znGXKHVfdUg",
  authDomain: "schooling-mex.firebaseapp.com",
  databaseURL: "https://schooling-mex.firebaseio.com",
  projectId: "schooling-mex",
  storageBucket: "schooling-mex.appspot.com",
  messagingSenderId: "115969086228"
};

if (!firebase.apps.length) {
  firebase.initializeApp(config);
}

const db = firebase.database();
const auth = firebase.auth();

export { db, auth, firebase };
