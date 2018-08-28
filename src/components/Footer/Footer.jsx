import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import ListItem from "@material-ui/core/ListItem";
import List from "@material-ui/core/List";

import * as strings from "../../application/constants/strings";
import * as routes from "../../application/constants/routes";

// core components
import footerStyle from "../../assets/jss/material-dashboard-react/components/footerStyle";

class Footer extends React.Component {
  render() {
    const { classes } = this.props;
    return (
      <footer className={classes.footer}>
        <div className={classes.container}>
          <div className={classes.left}>
            <List className={classes.list}>
              <ListItem className={classes.inlineBlock}>
                <Link to={routes.LANDING} className={classes.block}>
                  Principal
                </Link>
              </ListItem>
            </List>
          </div>
          <p className={classes.right}>
            <span>
              &copy; {1900 + new Date().getYear()}{" "} {strings.PROJECT}
            </span>
          </p>
        </div>
      </footer>
    );
  }
}

Footer.propTypes = {
  classes: PropTypes.object.isRequired
};

export default withStyles(footerStyle)(Footer);
