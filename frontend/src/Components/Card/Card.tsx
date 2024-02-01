//tsrafce
import React, { SyntheticEvent } from "react";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";

type Props = {
  id: string;
  searchResult: CompanySearch;
  OnSubmitPortfolio: (e: SyntheticEvent) => void;
};

const Card: React.FC<Props> = ({
  id,
  searchResult,
  OnSubmitPortfolio,
}: Props): JSX.Element => {
  return (
    <div className="Card">
      <img alt="company logo" />
      <div className="details">
        <h2>
          {searchResult.name} ({searchResult.symbol})
        </h2>
        <p>{searchResult.currency}</p>
      </div>
      <p className="infon">
        {searchResult.exchangeShortName} - {searchResult.stockExchange}
      </p>
      <AddPortfolio
        OnSubmitPortfolio={OnSubmitPortfolio}
        symbol={searchResult.symbol}
      />
    </div>
  );
};

export default Card;
