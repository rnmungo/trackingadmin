import {
  createContext,
  forwardRef,
  useCallback,
  useContext,
  useState,
} from 'react';
import { node } from 'prop-types';
import MuiAlert from '@mui/material/Alert';
import MuiSnackbar from '@mui/material/Snackbar';

const context = createContext({});

const Alert = forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export const useSnackbar = () => {
  const state = useContext(context);

  const success = message => {
    state.notify(message, 'success');
  };

  const error = message => {
    state.notify(message, 'error');
  };

  const caution = message => {
    state.notify(message, 'warning');
  };

  const info = message => {
    state.notify(message, 'info');
  };

  return { success, error, caution, info };
};

const SnackbarProvider = ({ children }) => {
  const [snackbar, setSnackbar] = useState({
    open: false,
    message: '',
    severity: 'string',
  });

  const notify = useCallback(
    (message, variant = 'default') =>
      setSnackbar({
        open: true,
        message,
        severity: variant,
      }),
    []
  );

  const handleClose = () => setSnackbar({ ...snackbar, open: false });

  return (
    <>
      <MuiSnackbar
        key="bottom center"
        autoHideDuration={20000}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
        open={snackbar?.open}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity={snackbar?.severity}>
          {snackbar?.message}
        </Alert>
      </MuiSnackbar>
      <context.Provider value={{ notify }}>{children}</context.Provider>
    </>
  );
};

SnackbarProvider.propTypes = {
  children: node,
};

export default SnackbarProvider;
