import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import InputAdornment from "@material-ui/core/InputAdornment";
// @material-ui/icons
import Email from "@material-ui/icons/Email";
// core components
import Header from "../../components/Header/LandingHeader";
import AnonymousHeaderLinks from "../../components/Header/AnonymousHeaderLinks";
import GridContainer from "../../components/Grid/GridContainer";
import GridItem from "../../components/Grid/GridItem";
import Button from "../../components/CustomButtons/Button";
import Card from "../../components/Card/Card";
import CardBody from "../../components/Card/CardBody";
import CardHeader from "../../components/Card/CardHeader";
import CardFooter from "../../components/Card/CardFooter";
import CustomInput from "../../components/CustomInput/CustomInput";

import * as strings from "../../application/constants/strings";
import * as routes from "../../application/constants/routes";

import { auth } from "../../firebase";

import "../../material-kit-react.css";

import passwordForgotPageStyle from "../../assets/jss/material-kit-react/views/passwordForgotPage";

import image from "../../assets/img/advise-advisor.jpg";

const byPropKey = (proppertyName, value) => () => ({
  [proppertyName]: value
});

const INITIAL_STATE = {
  email: "",
  error: null
};

class PasswordForgotPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      ...INITIAL_STATE
    };
  }

  onSubmit = event => {
    const { email } = this.state;

    const { history } = this.props;

    auth
      .doPasswordReset(email)
      .then(() => {
        this.setState({ ...INITIAL_STATE });
        history.push(routes.LOGIN);
      })
      .catch(error => {
        this.setState(byPropKey("error", error));
      });

    event.preventDefault();
  };

  render() {
    const { email, error } = this.state;

    const isInvalid = email === "";

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
              <GridItem xs={12} sm={12} md={6}>
                <Card className={classes[this.state.cardAnimaton]}>
                  <form className={classes.form} onSubmit={this.onSubmit}>
                    <CardHeader color="primary" className={classes.cardHeader}>
                      <h4>Recuperar contraseña</h4>
                    </CardHeader>
                    <CardBody>
                      <CustomInput
                        formControlProps={{
                          fullWidth: true,
                          className: classes.customInput
                        }}
                        inputProps={{
                          startAdornment: (
                            <InputAdornment
                              position="start"
                              className={classes.inputAdornment}
                            >
                              <Email className={classes.inputAdornmentIcon} />
                            </InputAdornment>
                          ),
                          placeholder: "Correo electrónico",
                          value: email,
                          onChange: event =>
                            this.setState(byPropKey("email", event.target.value))
                        }}
                      />
                    </CardBody>
                    <CardFooter className={classes.cardFooter}>
                      <div className={classes.center}>
                        <Button round color="primary" disabled={isInvalid}
                        type="submit"
                        onClick={this.onSubmit}>
                          Aceptar
                        </Button>
                      </div>
                    </CardFooter>

                    {error && (
                      <p className="text-center error-form without-line-height">
                        {error.message}
                      </p>
                    )}
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

export default withStyles(passwordForgotPageStyle)(PasswordForgotPage);
