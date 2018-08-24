import * as db from "../firebase/db";

export const doCreateUser = (id, username, email) =>
  db.doCreateUser(id, username, email);

export const onceGetUsers = () => db.onceGetUsers();
