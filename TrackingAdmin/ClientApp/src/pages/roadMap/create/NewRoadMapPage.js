import MuiBox from '@mui/material/Box';
import MuiTypography from '@mui/material/Typography';
import { RoadMapFormContainer } from '../../../containers/roadMapForm';

const NewRoadMapPage = () => (
  <>
    <MuiTypography component="h1" variant="h4">Hoja de Ruta</MuiTypography>
    <MuiBox sx={{ py: 4 }}>
      <RoadMapFormContainer />
    </MuiBox>
  </>
);

export default NewRoadMapPage;
