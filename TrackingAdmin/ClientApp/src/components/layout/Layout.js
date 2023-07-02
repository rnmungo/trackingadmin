import { node } from 'prop-types';
import MuiCssBaseline from '@mui/material/CssBaseline';
import MuiGrid from '@mui/material/Grid';
import { createTheme, ThemeProvider as MuiThemeProvider } from '@mui/material/styles';
import { QueryProvider } from '../../contexts/query';
import { SnackbarProvider } from '../../contexts/snackbar';
import { THEME_CONFIGURATION } from '../../constants';
import { Drawer } from '../drawer';

const Layout = ({ children }) => (
  <QueryProvider>
    <MuiThemeProvider theme={createTheme(THEME_CONFIGURATION)}>
      <MuiCssBaseline />
      <Drawer>
        <MuiGrid
          component="main"
          sx={{
            bgcolor: 'background.default',
            maxHeight: '100vh',
            overflowY: 'hidden',
            overflowX: 'hidden',
          }}
        >
          <SnackbarProvider>
            {children}
          </SnackbarProvider>
        </MuiGrid>
      </Drawer>
    </MuiThemeProvider>
  </QueryProvider>
);

Layout.propTypes = {
  children: node.isRequired,
};

export default Layout;
