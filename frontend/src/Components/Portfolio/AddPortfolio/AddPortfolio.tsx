import React, { SyntheticEvent } from "react";

interface Props {
  OnSubmitPortfolio: (e: SyntheticEvent) => void;
  symbol: string;
}

const AddPortfolio = ({ OnSubmitPortfolio, symbol }: Props) => {
  return (
    <form onSubmit={OnSubmitPortfolio}>
      <input readOnly={true} hidden={true} value={symbol} />
      <button type="submit">AddAyo</button>
    </form>
  );
};

export default AddPortfolio;
