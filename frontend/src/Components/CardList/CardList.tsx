import React, { SyntheticEvent } from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";
import { ADDRGETNETWORKPARAMS } from "dns";

interface Props {
  searchResults: CompanySearch[];
  OnSubmitPortfolio: (e: SyntheticEvent) => void;
}

const CardList: React.FC<Props> = ({
  searchResults: searchResults,
  OnSubmitPortfolio: OnSubmitPortfolio,
}: Props): JSX.Element => {
  return (
    <>
      {searchResults.length > 0 ? (
        searchResults.map((result) => {
          return (
            <Card
              id={result.symbol}
              key={uuidv4()}
              searchResult={result}
              OnSubmitPortfolio={OnSubmitPortfolio}
            />
          );
        })
      ) : (
        <h1>No Results found</h1>
      )}
    </>
  );
};

export default CardList;
