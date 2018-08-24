import Dashboard from "../layouts/Dashboard/App.jsx";
import Landing from "../views/Landing/LandingPage.jsx";
import Login from "../views/Login/LoginPage.jsx";

const indexRoutes = [
  { path: "/login", component: Login },
  { path: "/dashboard", component: Dashboard },
  { path: "/", component: Landing }
];

export default indexRoutes;
