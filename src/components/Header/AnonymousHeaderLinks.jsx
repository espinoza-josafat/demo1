/*eslint-disable*/
import React from "react";

// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";

// core components
import Button from "../../components/CustomButtons/Button.jsx";

import * as routes from "../../application/constants/routes";

import headerLinksStyle from "../../assets/jss/material-kit-react/components/headerLinksStyle.jsx";

function AnonymousHeaderLinks({ ...props }) {
  const { classes } = props;
  return (
    <List className={classes.list}>
      <ListItem className={classes.listItem}>
        <Button
          href={routes.REGISTER}
          color="transparent"
          className={classes.navLink}
        >
          Registrate
        </Button>
      </ListItem>
      <ListItem className={classes.listItem}>
        <Button
          href={routes.LOGIN}
          color="transparent"
          className={classes.navLink}
        >
          Inicia sesión
        </Button>
      </ListItem>
    </List>
  );
}

export default withStyles(headerLinksStyle)(AnonymousHeaderLinks);
