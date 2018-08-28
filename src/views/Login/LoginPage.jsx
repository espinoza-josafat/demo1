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

import * as fb from "firebase";
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
    
    this.state = {
      ...INITIAL_STATE
    };

    this.loginWithFacebook = this.loginWithFacebook.bind(this);
    this.loginWithGmail = this.loginWithGmail.bind(this);
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
  }

  loginWithFacebook = event => {
    let that = this;
    const { history } = that.props;

    var provider = new fb.auth.FacebookAuthProvider();

    auth.doSignInWithPopup(provider).then(function(authResult) {
      users
      .doCreateUser(
        authResult.user.uid,
        authResult.user.displayName,
        authResult.user.email
      )
      .then(() => {
        that.setState({ ...INITIAL_STATE });
        history.push(routes.DASHBOARD);
      })
      .catch(error => {
        that.setState(byPropKey("error", error));
      });
    })
    .catch(error => {
      that.setState(byPropKey("error", error));
    });
  }

  loginWithGmail = event => {
    let that = this;

    const { history } = that.props;

    var provider = new fb.auth.GoogleAuthProvider();

    auth.doSignInWithPopup(provider).then(function(authResult) {
      users
      .doCreateUser(
        authResult.user.uid,
        authResult.user.displayName,
        authResult.user.email
      )
      .then(() => {
        that.setState({ ...INITIAL_STATE });
        history.push(routes.DASHBOARD);
      })
      .catch(error => {
        that.setState(byPropKey("error", error));
      });
    })
    .catch(error => {
      that.setState(byPropKey("error", error));
    });
  }

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
              <GridItem xs={12} sm={12} md={5}>
                <Card className={classes[this.state.cardAnimaton]}>
                  <form className={classes.form}>
                    <CardHeader color="primary" className={classes.cardHeader}>
                      <h4>Inicia sesión</h4>
                      <div className={classes.socialLine}>
                        <Button
                          justIcon
                          color="transparent"
                          onClick={this.loginWithFacebook}
                        >
                          <i className={"fab fa-facebook"} />
                        </Button>
                        <Button
                          justIcon
                          color="transparent"
                          onClick={this.loginWithGmail}
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

export default withStyles(loginPageStyle)(LoginPage);
