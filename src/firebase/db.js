/*import { db } from "./firebase";

export const doCreateUser = (id, username, email) => {
  db.ref(`users/${id}`).set({
    username,
    email
  });
}

export const onceGetUsers = () => db.ref(`users`).once(`value`);*/

/*import { db } from "./firebase";

export const doCreateUser = (id, username, email) => {
  var idProfileDocente = "-LKZwy6FpZ4ZkdsU4aX7";
  // Write the new post's data simultaneously in the posts list and the user's post list.
  var updates = {};

  updates["users/" + id] = {
    "email": email,
    "username": username
  };

  updates["users/" + id].profiles = {};
  updates["users/" + id].profiles[idProfileDocente] = true;

  updates["profiles/" + idProfileDocente + "/users"] = {};
  updates["profiles/" + idProfileDocente + "/users"][id] = true;

  return db.ref().update(updates);
}

export const onceGetUsers = () => db.ref(`users`).once(`value`);*/

/*import { db } from "./firebase";

export const doCreateUser = (id, username, email) => {
  var idProfileDocente = "-LKZwy6FpZ4ZkdsU4aX7";

  var user = {};
  user["username"] = username;
  user["email"] = email;
  user["profiles"] = {};
  user["profiles"][idProfileDocente] = true;

  return Promise.all([db.ref(`users/${id}`).set(user), db.ref(`profiles/${idProfileDocente}/users/${id}`).set(true)]);
}

export const onceGetUsers = () => db.ref(`users`).once(`value`);*/

import { db } from "./firebase";

export const doCreateUser = (id, username, email) => {
  var idProfileDocente = "-LKZwy6FpZ4ZkdsU4aX7";
  
  // Write the new post's data simultaneously in the posts list and the user's post list.
  var updates = {};

  updates["users/" + id] = {
    "email": email,
    "username": username
  };

  updates["users/" + id].profiles = {};
  updates["users/" + id].profiles[idProfileDocente] = true;

  updates["profiles/" + idProfileDocente + "/users/" + id] = true;

  return db.ref().update(updates);
}

export const onceGetUsers = () => db.ref(`users`).once(`value`);
