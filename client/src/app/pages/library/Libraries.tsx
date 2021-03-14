import { DetailsList, DetailsListLayoutMode, IColumn, Spinner, SpinnerSize } from '@fluentui/react';
import { ApiClient, ILibraryDto } from 'app/generated/backend';
import { IButtonProps, Icon, Image, initializeIcons, Nav, Text } from '@fluentui/react';
import React, { useEffect, useState } from 'react';
import Form from '../../components/Form';
import { render } from 'react-dom';
import { Router, RouteComponentProps, navigate } from '@reach/router';
import { Link } from 'react-router-dom';

const Libraries: React.FC = () => {
    const [data, setData] = useState({
        libraries: [] as ILibraryDto[],
        isFetching: false
    });

    const libraryKeys: ILibraryDto = {
        id: null,
        name: '',
        address_AddressLine1: '',
        address_AddressLine2: '',
        address_City: '',
        address_StateProvince: '',
        address_Country: '',
        address_ZipCode: '',

        createdDate: null,
        updatedDate: null
    };

    const columns = Object.keys(libraryKeys).map(
        (key): IColumn => {
            return {
                key,
                name: key
                    .replaceAll('address_', '')
                    .replace(/([A-Z])/g, ' $1')
                    .replace(/^./, (str: string) => {
                        return str.toUpperCase();
                    }),
                fieldName: key,
                minWidth: 100,
                maxWidth: 200,
                isResizable: true
            };
        }
    );

    useEffect(() => {
        const fetchData = async () => {
            try {
                setData({ libraries: data.libraries, isFetching: true });
                const result = await new ApiClient(process.env.REACT_APP_API_BASE).library_GetAllLibraries();
                setData({ libraries: result, isFetching: false });
            } catch (e) {
                console.log(e);
                setData({ libraries: data.libraries, isFetching: false });
            }
        };

        fetchData();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <div>
            <h2>Libraries</h2>
            <DetailsList
                items={data.libraries.map((library) => {
                    return {
                        ...library,
                        createdDate: library.createdDate.toLocaleString(),
                        updatedDate: library.updatedDate.toLocaleString()
                    };
                })}
                columns={columns}
                layoutMode={DetailsListLayoutMode.justified}
            />
            {data.isFetching && <Spinner size={SpinnerSize.large} />}
            <button type="submit" className="btn btn-light" >
            <Link to="/library_form">Add</Link>
            </button> 
        </div>
    );
};

export default Libraries;
