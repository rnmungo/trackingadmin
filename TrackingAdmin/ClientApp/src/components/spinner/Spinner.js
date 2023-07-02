import MuiBackdrop from '@mui/material/Backdrop';
import MuiGrid from '@mui/material/Grid';
import MuiTypography from '@mui/material/Typography';
import MuiCircularProgress from '@mui/material/CircularProgress';
import { styled } from '@mui/material/styles';
import { bool, string } from 'prop-types';

export const Backdrop = styled(MuiBackdrop)(({ theme }) => ({
  color: theme.palette.secondary.contrastText,
  textAlign: 'center',
  zIndex: 'fab',
}));

const Spinner = ({ label = '', loading = false }) => (
  <Backdrop open={loading}>
    <MuiGrid container justifyContent="center" alignItems="center" gap={4}>
      <MuiGrid item xs={12}>
        <MuiCircularProgress color="inherit" size={60} />
      </MuiGrid>
      {label && (
        <MuiGrid item xs={12}>
          <MuiTypography variant="h5" align="center">
            {label}
          </MuiTypography>
        </MuiGrid>
      )}
    </MuiGrid>
  </Backdrop>
);

Spinner.propTypes = {
  label: string,
  loading: bool,
};

export default Spinner;
