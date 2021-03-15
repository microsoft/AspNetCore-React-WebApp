import React, { MouseEvent, useState } from 'react';
import { ApiClient, ILibraryDto, LibraryDto } from 'app/generated/backend';

import { Link } from 'react-router-dom';

const Form: React.FC = () => {
    const [library, setLibrary] = useState({
        name: '',
        address_AddressLine1: '',
        address_AddressLine2: '',
        address_City: '',
        address_StateProvince: '',
        address_ZipCode: '',
        address_Country: ''
    });
    const onClickHandler = (e: React.ChangeEvent<HTMLInputElement>): void => {
        setLibrary({
            ...library,
            [e.target.name]: e.target.value
        });
    };
    const onSubmitHandler = (e: MouseEvent): void => {
        e.preventDefault();

        const fetchData = async () => {
            try {
                const result = await new ApiClient(process.env.REACT_APP_API_BASE).library_CreateLibrary(
                    LibraryDto.fromJS(library)
                );
            } catch (e) {
                console.log(e);
            }
        };

        fetchData();

        setLibrary({
            name: '',
            address_AddressLine1: '',
            address_AddressLine2: '',
            address_City: '',
            address_StateProvince: '',
            address_ZipCode: '',
            address_Country: ''
        });
    };
    return (
        <>
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className="form-control"
                    id="floatingInput"
                    name="name"
                    value={library.name}
                    onChange={onClickHandler}
                    placeholder="Name"
                    required
                />
            </div>
            <div className="form-floating mb-3 ">
                <input
                    type="text"
                    className="form-control"
                    id="floatingInput"
                    name="address_AddressLine1"
                    value={library.address_AddressLine1}
                    onChange={onClickHandler}
                    placeholder="Address line 1"
                    required
                />
            </div>
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className="form-control"
                    id="floatingInput"
                    name="address_AddressLine2"
                    value={library.address_AddressLine2}
                    onChange={onClickHandler}
                    placeholder="Address line 2"
                />
            </div>
            <div className="form-floating mb-3 ">
                <input
                    type="text"
                    className="form-control"
                    id="floaingCity"
                    name="address_City"
                    value={library.address_City}
                    onChange={onClickHandler}
                    placeholder="City"
                    required
                />
            </div>
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className="form-control"
                    id="floatingState"
                    name="address_StateProvince"
                    value={library.address_StateProvince}
                    onChange={onClickHandler}
                    placeholder="State"
                    required
                />
            </div>

            <div className="form-floating mb-3">
                <input
                    type="text"
                    className="form-control"
                    id="floatingZipCode"
                    name="address_ZipCode"
                    value={library.address_ZipCode}
                    onChange={onClickHandler}
                    placeholder="ZipCode"
                    required
                />
            </div>
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className="form-control"
                    id="floatingCountry"
                    name="address_Country"
                    value={library.address_Country}
                    onChange={onClickHandler}
                    placeholder="Country"
                    required
                />
            </div>

            <button type="submit" className="btn btn-light" onClick={onSubmitHandler}>
                <Link to="/libraries">Save</Link>
            </button>
        </>
    );
};

export default Form;
