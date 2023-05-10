import { useCallback, useMemo, useState } from 'react';
import MaterialReactTable from 'material-react-table';
import type { MaterialReactTableProps, MRT_Cell, MRT_ColumnDef, MRT_Row } from 'material-react-table';
import {
	Box,
	Button,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	IconButton,
	Stack,
	TextField,
	Tooltip,
} from '@mui/material';
import { Delete, Edit, DirectionsBoat } from '@mui/icons-material';
import { Ship } from '../../types/Ship';
import { SHIP_NAME_REGEX } from '../../utils/constants';
import { hasAtLeastOneValue } from '../../utils/helpers';

interface MaterialTableProps {
	tableData: Array<Ship>;
	setTableData: Function;
	isLoading: boolean;
	handleCreateNewRow: (values: Ship) => void;
	handleDeleteRow: (row: MRT_Row<Ship>) => void;
	handleUpdateRow: (row: any, values: any) => void;
}

const MaterialTable = ({
	tableData,
	setTableData,
	isLoading,
	handleCreateNewRow,
  handleUpdateRow,
	handleDeleteRow,
}: MaterialTableProps) => {
	const [createModalOpen, setCreateModalOpen] = useState(false);
	// const [tableData, setTableData] = useState<Ship[]>(() => data);
	const [validationErrors, setValidationErrors] = useState<{
		[cellId: string]: string;
	}>({});

	const handleSaveRowEdits: MaterialReactTableProps<Ship>['onEditingRowSave'] = async ({
		exitEditingMode,
		row,
		values,
	}) => {
		if (!hasAtLeastOneValue(validationErrors)) {
        handleUpdateRow(row, values);
				exitEditingMode();
		}
	};

	const handleCancelRowEdits = () => {
		setValidationErrors({});
	};

	const validateField = (name: string, value: string) => {
		let message = '';

		// Check if values are valid
		if (name === 'code' && !SHIP_NAME_REGEX.test(value)) {
			message = 'Code format is not correct (e.g. AAAA-1111-A1)';
		}
		if (name === 'name' && !value.trim()) {
			message = 'Name is required';
		}
		if (name === 'length' && +value < 1) {
			message = 'Length is not valid';
		}
		if (name === 'width' && +value < 1) {
			message = 'Width is not valid';
		}

		setValidationErrors((prevState: any) => ({
			...prevState,
			[name]: message,
		}));
	};

	const getCommonEditTextFieldProps = useCallback(
		(cell: MRT_Cell<Ship>): MRT_ColumnDef<Ship>['muiTableBodyCellEditTextFieldProps'] => {
			return {
				error: !!validationErrors[cell.column.id],
				helperText: validationErrors[cell.column.id],
				onBlur: (e) => validateField(e.target.name, e.target.value),
			};
		},
		[validationErrors]
	);

	const columns = useMemo<MRT_ColumnDef<Ship>[]>(
		() => [
			// {
			// 	accessorKey: 'id',
			// 	header: 'ID',
			// 	enableColumnOrdering: false,
			// 	enableEditing: false, //disable editing on this column
			// 	enableSorting: false,
			// 	size: 80,
			// },
			{
				accessorKey: 'code',
				header: 'Code',
				size: 140,
				muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
					...getCommonEditTextFieldProps(cell),
					required: true,
					variant: 'outlined',
				}),
			},
			{
				accessorKey: 'name',
				header: 'Name',
				size: 140,
				muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
					...getCommonEditTextFieldProps(cell),
					required: true,
					variant: 'outlined',
				}),
			},
			{
				accessorKey: 'length',
				header: 'Length',
				size: 140,
				muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
					...getCommonEditTextFieldProps(cell),
					type: 'number',
					required: true,
					variant: 'outlined',
				}),
			},
			{
				accessorKey: 'width',
				header: 'Width',
				muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
					...getCommonEditTextFieldProps(cell),
					type: 'number',
					required: true,
					variant: 'outlined',
				}),
			},
		],
		[getCommonEditTextFieldProps]
		// []
	);

	return (
		<>
			<MaterialReactTable
				displayColumnDefOptions={{
					'mrt-row-actions': {
						muiTableHeadCellProps: {
							align: 'center',
						},
						size: 120,
					},
				}}
				columns={columns}
				data={tableData}
				state={{
					isLoading,
				}}
				editingMode='modal' //default
				enableColumnOrdering
				enableEditing
				onEditingRowSave={handleSaveRowEdits}
				onEditingRowCancel={handleCancelRowEdits}
				renderRowActions={({ row, table }) => (
					<Box sx={{ display: 'flex', gap: '1rem' }}>
						<Tooltip arrow placement='left' title='Edit'>
							<IconButton onClick={() => table.setEditingRow(row)}>
								<Edit />
							</IconButton>
						</Tooltip>
						<Tooltip arrow placement='right' title='Delete'>
							<IconButton color='error' onClick={() => handleDeleteRow(row)}>
								<Delete />
							</IconButton>
						</Tooltip>
					</Box>
				)}
				renderTopToolbarCustomActions={() => (
					<Button
						color='primary'
						onClick={() => setCreateModalOpen(true)}
						startIcon={<DirectionsBoat />}
						variant='contained'
					>
						Add New Ship
					</Button>
				)}
			/>
			<CreateNewAccountModal
				columns={columns}
				open={createModalOpen}
				onClose={() => {
					setCreateModalOpen(false);
					setValidationErrors({});
				}}
				onSubmit={handleCreateNewRow}
				validateField={validateField}
				validationErrors={validationErrors}
			/>
		</>
	);
};

interface CreateModalProps {
	columns: MRT_ColumnDef<Ship>[];
	onClose: () => void;
	onSubmit: (values: Ship) => void;
	open: boolean;
	validateField: Function;
	validationErrors: any;
}

//example of creating a mui dialog modal for creating new rows
export const CreateNewAccountModal = ({
	open,
	columns,
	onClose,
	onSubmit,
	validateField,
	validationErrors,
}: CreateModalProps) => {
	const [values, setValues] = useState<any>(() =>
		columns.reduce((acc, column) => {
			acc[column.accessorKey ?? ''] = '';
			return acc;
		}, {} as any)
	);

	const handleSubmit = () => {
		if (Object.keys(validationErrors).length === 4 && !hasAtLeastOneValue(validationErrors)) {
			onSubmit(values);
			onClose();
		} else {
			return;
		}
	};

	return (
		<Dialog open={open}>
			<DialogTitle textAlign='center'>Add New Ship</DialogTitle>
			<DialogContent style={{ paddingTop: '0.5rem' }}>
				<form onSubmit={(e) => e.preventDefault()}>
					<Stack
						sx={{
							width: '100%',
							minWidth: { xs: '300px', sm: '360px', md: '400px' },
							gap: '1.5rem',
						}}
					>
						{columns.map((column) => (
							<TextField
								error={Boolean(validationErrors[column.accessorKey!])}
								helperText={validationErrors[column.accessorKey!]}
								key={column.accessorKey}
								type={['length', 'width'].includes(column.accessorKey!) ? 'number' : 'text'}
								label={column.header}
								name={column.accessorKey}
								required
								onBlur={(e) => validateField(e.target.name, e.target.value)}
								onChange={(e) => setValues({ ...values, [e.target.name]: e.target.value })}
							/>
						))}
					</Stack>
				</form>
			</DialogContent>
			<DialogActions sx={{ p: '1.25rem' }}>
				<Button onClick={onClose}>Cancel</Button>
				<Button color='primary' onClick={handleSubmit} variant='contained'>
					Submit
				</Button>
			</DialogActions>
		</Dialog>
	);
};

// const validateRequired = (value: string) => !!value.length;
// const validateEmail = (email: string) =>
// 	!!email.length &&
// 	email
// 		.toLowerCase()
// 		.match(
// 			/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
// 		);
// const validateAge = (age: number) => age >= 18 && age <= 50;

export default MaterialTable;
