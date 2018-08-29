import React from "react";

import Wizard from "../../components/Wizard/Wizard.jsx";
import GridContainer from "../../components/Grid/GridContainer.jsx";
import GridItem from "../../components/Grid/GridItem.jsx";

import Step1InfoUbicacion from "./Steps/Step1InfoUbicacion.jsx";
/*import Step2InfoAcademico from "./Steps/Step2InfoAcademico.jsx";
import Step3InfoGeneral from "./Steps/Step3InfoGeneral.jsx";*/

import withAuthorization from "../../application/withAuthorization";

class MisAulasPage extends React.Component {
  render() {
    return (
      <GridContainer justify="center">
        <GridItem xs={12} sm={8}>
          <Wizard
            validate
            steps={[
              {
                stepName: "Ubicación",
                stepComponent: Step1InfoUbicacion,
                stepId: "ubicacion"
              }
            ]}
            title="Crea un aula"
            subtitle="En este apartado podrás crear las aulas donde impartes clase"
          />
        </GridItem>
      </GridContainer>
    );
  }
}

const authCondition = authUser => !!authUser;

export default withAuthorization(authCondition)(MisAulasPage);
