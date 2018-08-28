import Dashboard from "../layouts/Dashboard/App";
import Landing from "../views/Landing/LandingPage";
import Login from "../views/Login/LoginPage";
import Register from "../views/Register/RegisterPage";
import PasswordForgot from "../views/PasswordForgot/PasswordForgotPage";

import * as routes from "../application/constants/routes";

const indexRoutes = [
  { path: routes.LOGIN, component: Login },
  { path: routes.REGISTER, component: Register },
  { path: routes.PASSWORD_FORGET, component: PasswordForgot },
  { path: routes.DASHBOARD, component: Dashboard },
  { path: routes.LANDING, component: Landing }
];

export default indexRoutes;
