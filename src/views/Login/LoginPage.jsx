import React from "react";
import { Link } from "react-router-dom";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
import Email from "@material-ui/icons/Email";
// core components
import Header from "../../components/Header/LandingHeader.jsx";
import AnonymousHeaderLinks from "../../components/Header/AnonymousHeaderLinks.jsx";
import Footer from "../../components/Footer/Footer.jsx";
import GridContainer from "../../components/Grid/GridContainer.jsx";
import GridItem from "../../components/Grid/GridItem.jsx";
import Button from "../../components/CustomButtons/Button.jsx";
import Card from "../../components/Card/Card.jsx";
import CardBody from "../../components/Card/CardBody.jsx";
import CardHeader from "../../components/Card/CardHeader.jsx";
import CardFooter from "../../components/Card/CardFooter.jsx";
import CustomInput from "../../components/CustomInput/CustomInput.jsx";

import * as strings from "../../application/constants/strings.js";
import * as routes from "../../application/constants/routes";

//import { history } from "history";

import * as fb from "firebase";
import StyledFirebaseAuth from "react-firebaseui/StyledFirebaseAuth";
import { auth } from "../../firebase/index";
import * as users from "../../bussiness/users";

import "../../material-kit-react.css";

import loginPageStyle from "../../assets/jss/material-kit-react/views/loginPage.jsx";

import image from "../../assets/img/advise-advisor.jpg";

const byPropKey = (proppertyName, value) => () => ({
  [proppertyName]: value
});

const INITIAL_STATE = {
  email: "",
  password: "",
  error: null
};

class LoginPage extends React.Component {
  constructor(props) {
    super(props);
    // we use this to make the card to appear after the page has been rendered
    this.state = {
      cardAnimaton: "cardHidden",
      ...INITIAL_STATE
    };
  }

  componentDidMount() {
    // we add a hidden class to the card and after 700 ms we delete it and the transition appears
    setTimeout(
      function() {
        this.setState({ cardAnimaton: "" });
      }.bind(this),
      700
    );
  }

  onSubmit = event => {
    const { email, password } = this.state;

    const { history } = this.props;

    auth
      .doSignInWithEmailAndPassword(email, password)
      .then(() => {
        this.setState({ ...INITIAL_STATE });
        history.push(routes.DASHBOARD);
      })
      .catch(error => {
        this.setState(byPropKey("error", error));
      });

    event.preventDefault();
  };

  render() {
    const { email, password, error } = this.state;

    const isValid = password === "" || email === "";

    const { classes, ...rest } = this.props;

    return (
      <div>
        <Header
          absolute
          color="transparent"
          brand={strings.PROJECT_MAYUS}
          rightLinks={<AnonymousHeaderLinks />}
          {...rest}
        />
        <div
          className={classes.pageHeader}
          style={{
            backgroundImage: "url(" + image + ")",
            backgroundSize: "cover",
            backgroundPosition: "top center"
          }}
        >
          <div className={classes.container}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={4}>
                <Card className={classes[this.state.cardAnimaton]}>
                  <form className={classes.form}>
                    <CardHeader color="primary" className={classes.cardHeader}>
                      <h4>Inicia sesión</h4>
                      <div className={classes.socialLine}>
                        <Button
                          justIcon
                          color="transparent"
                          onClick={e => e.preventDefault()}
                        >
                          <i className={"fab fa-facebook"} />
                        </Button>
                        <Button
                          justIcon
                          color="transparent"
                          onClick={e => e.preventDefault()}
                        >
                          <i className={"fab fa-google-plus-g"} />
                        </Button>
                      </div>
                    </CardHeader>
                    <CardBody>
                      <CustomInput
                        labelText="Correo electrónico"
                        id="email"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "email",
                          value: email,
                          onChange: event =>
                            this.setState(
                              byPropKey("email", event.target.value)
                            ),
                          endAdornment: (
                            <InputAdornment position="end">
                              <Email className={classes.inputIconsColor} />
                            </InputAdornment>
                          )
                        }}
                      />
                      <CustomInput
                        labelText="Contraseña"
                        id="password"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "password",
                          value: password,
                          onChange: event =>
                            this.setState(
                              byPropKey("password", event.target.value)
                            ),

                          endAdornment: (
                            <InputAdornment position="end">
                              <Icon className={classes.inputIconsColor}>
                                lock_outline
                              </Icon>
                            </InputAdornment>
                          )
                        }}
                      />
                    </CardBody>
                    <CardFooter className={classes.cardFooter}>
                      <Button
                        disabled={isValid}
                        type="submit"
                        onClick={this.onSubmit}
                        simple
                        color="primary"
                        size="lg"
                      >
                        Listo
                      </Button>
                    </CardFooter>

                    {error && (
                      <p className="text-center error-form without-line-height">
                        {error.message}
                      </p>
                    )}

                    <LoginLink />
                    <PasswordForgetLink />
                  </form>
                </Card>
              </GridItem>
            </GridContainer>
          </div>
          <StyledFirebaseAuthForm history={this.props.history} />
          <Footer whiteFont />
        </div>
      </div>
    );
  }
}

class LoginLink extends React.Component {
  render() {
    const styleLoginLink = {
      textAlign: "center",
      marginBottom: "0",
      marginTop: "10px"
    };

    return (
      <p style={styleLoginLink}>
        No tienes una cuenta <Link to={routes.REGISTER}>Regístrate</Link>
      </p>
    );
  }
}

class PasswordForgetLink extends React.Component {
  render() {
    const stylePasswordForgetLink = {
      textAlign: "center",
      marginBottom: "20px",
      marginTop: "0"
    };

    return (
      <p style={stylePasswordForgetLink}>
        <Link to={routes.PASSWORD_FORGET}>¿Olvidaste tu contraseña?</Link>
      </p>
    );
  }
}

class StyledFirebaseAuthForm extends React.Component {
  uiConfig = {
    signInFlow: "popup",
    signInOptions: [
      fb.auth.GoogleAuthProvider.PROVIDER_ID,
      fb.auth.FacebookAuthProvider.PROVIDER_ID
    ],
    callbacks: {
      signInSuccessWithAuthResult: (authResult, redirectUrl) => {
        //console.log("authResult", authResult);

        users
          .doCreateUser(
            authResult.user.uid,
            authResult.user.displayName,
            authResult.user.email
          )
          .then(() => {
            this.onSubmit();
          })
          .catch(error => {
            this.setState(byPropKey("error", error));
          });

        return false;
      }
    }
  };

  onSubmit = () => {
    const { history } = this.props;
    history.push(routes.DASHBOARD);
  };

  render() {
    return (
      <StyledFirebaseAuth uiConfig={this.uiConfig} firebaseAuth={fb.auth()} />
    );
  }
}

export default withStyles(loginPageStyle)(LoginPage);
