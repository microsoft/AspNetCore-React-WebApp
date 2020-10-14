import { DetailsList, DetailsListLayoutMode, IColumn, Spinner, SpinnerSize } from '@fluentui/react';
import { ApiClient, IGroupDto } from 'app/generated/backend';
import React, { useEffect, useState } from 'react';

const Groups: React.FC = () => {
    const [data, setData] = useState({
        groups: [] as IGroupDto[],
        isFetching: false
    });

    const groupKeys: IGroupDto = {
        id: null,
        name: '',
        isActive: false,
        createdDate: null,
        updatedDate: null
    };

    const columns = Object.keys(groupKeys).map(
        (key): IColumn => {
            return {
                key,
                name: key.replace(/([A-Z])/g, ' $1').replace(/^./, (str: string) => {
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
                setData({ groups: data.groups, isFetching: true });
                const result = await new ApiClient(process.env.REACT_APP_API_BASE).groups_GetAllGroups();
                setData({ groups: result, isFetching: false });
            } catch (e) {
                console.log(e);
                setData({ groups: data.groups, isFetching: false });
            }
        };

        fetchData();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <h2>Groups</h2>
            <DetailsList
                items={data.groups.map((group) => {
                    return {
                        ...group,
                        createdDate: group.createdDate.toLocaleString(),
                        updatedDate: group.updatedDate.toLocaleString(),
                        isActive: group.isActive.toString()
                    };
                })}
                columns={columns}
                layoutMode={DetailsListLayoutMode.justified}
            />
            {data.isFetching && <Spinner size={SpinnerSize.large} />}
        </>
    );
};

export default Groups;
