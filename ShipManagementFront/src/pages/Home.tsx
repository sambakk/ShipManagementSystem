/** @jsxImportSource @emotion/react */
import React, { useCallback } from 'react';
import { Typography, Snackbar, Alert } from '@mui/material';
import { useEffect, useState } from 'react';
import { Helmet } from 'react-helmet-async';

import { MRT_Row } from 'material-react-table';

import { API_ERROR_MSG, APP_TITLE, PAGE_TITLE_HOME, PAGE_TITLE_SHIPS_OPS } from '../utils/constants';
import MaterialTable from '../components/Tables/MaterialTable';
import { Ship } from '../types/Ship';
import ShipService from '../services/ShipService';

export const Home = () => {
	const [data, setData] = useState<Ship[]>([]);
	const [isLoading, setIsLoading] = useState<boolean>(false);
	const [openSnackBar, setOpenSnackBar] = useState(false);
	const [snackBarMessage, setSnackBarMessage] = useState('');

	/**
	 * Handle Snackbar notification close
	 * @param event
	 * @param reason
	 * @returns
	 */
	const handleCloseSnackBar = (event?: React.SyntheticEvent | Event, reason?: string) => {
		if (reason === 'clickaway') {
			return;
		}
		setOpenSnackBar(false);
	};

	const showErrorMessage = (e: any) => {
		if (e?.response?.data?.error) {
			setSnackBarMessage(e.response.data.error);
		} else {
			setSnackBarMessage(API_ERROR_MSG);
		}
		setOpenSnackBar(true);
	};

	/**
	 * Fetch initial data
	 */
	useEffect(() => {
		const getAllShips = () => {
			setIsLoading(true);
			ShipService.getAll()
				.then((response: any) => {
					setData(response.data);
				})
				.catch((e: any) => {
					showErrorMessage(e);
				})
				.finally(() => setIsLoading(false));
		};

		getAllShips();
	}, []);

	/**
	 * Create new ship
	 * @param values
	 */
	const handleCreateNewRow = (values: Ship) => {
		ShipService.create(values)
			.then((response: any) => {
				data.push(values);
				setData([...data]);
			})
			.catch((e: any) => {
				showErrorMessage(e);
			})
			.finally(() => setIsLoading(false));
	};

	const handleDeleteRow = useCallback(
		(row: MRT_Row<Ship>) => {
			if (!confirm(`Are you sure you want to delete ${row.original.name} `)) {
				return;
			}

			ShipService.delete(row.original.id)
				.then((response: any) => {
					data.splice(row.index, 1);
					setData([...data]);
				})
				.catch((e: any) => {
					showErrorMessage(e);
				})
				.finally(() => setIsLoading(false));
		},
		[data]
	);

	const handleUpdateRow = useCallback(
		(row: any, values: any) => {
			ShipService.update(values, row.original.id)
				.then((response: any) => {
					data[row.index] = values;
					setData([...data]);
				})
				.catch((e: any) => {
					showErrorMessage(e);
				})
				.finally(() => setIsLoading(false));
		},
		[data]
	);

	return (
		<>
			<Helmet>
				<title>
					{PAGE_TITLE_HOME} | {APP_TITLE}
				</title>
			</Helmet>
			<Typography color='primary' variant='h4'>{PAGE_TITLE_SHIPS_OPS}</Typography>
			<br />
			<MaterialTable
				tableData={data}
				setTableData={setData}
				isLoading={isLoading}
				handleCreateNewRow={handleCreateNewRow}
				handleDeleteRow={handleDeleteRow}
				handleUpdateRow={handleUpdateRow}
			/>
			<Snackbar
				open={openSnackBar}
				autoHideDuration={6000}
				onClose={handleCloseSnackBar}
				anchorOrigin={{
					vertical: 'bottom',
					horizontal: 'center',
				}}
			>
				<Alert onClose={handleCloseSnackBar} severity='error' sx={{ width: '100%' }}>
					{snackBarMessage}
				</Alert>
			</Snackbar>
		</>
	);
};
