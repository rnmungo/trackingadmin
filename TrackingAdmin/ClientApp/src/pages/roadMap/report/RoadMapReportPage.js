import MuiBox from '@mui/material/Box';
import MuiTypography from '@mui/material/Typography';
import { ReportTableContainer } from '../../../containers/reportTable';

const RoadMapReportPage = () => (
  <>
    <MuiTypography component="h1" variant="h4">Seguimiento de Hojas de Ruta</MuiTypography>
    <MuiBox sx={{ py: 4 }}>
      <ReportTableContainer />
    </MuiBox>
  </>
);

export default RoadMapReportPage;
