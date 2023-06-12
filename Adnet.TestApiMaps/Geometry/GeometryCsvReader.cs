using Adnet.TestApiMaps.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Adnet.TestApiMaps
{
    public class GeometryCsvReader
	{

		public List<Geometry> Read( StreamReader stream )
		{
			List<Geometry> ret = new List<Geometry>();

			List<Guid> added = new List<Guid>();

			//Skip header
			stream.ReadLine();

			while ( !stream.EndOfStream )
			{
				string line = stream.ReadLine();
				string guid = SplitByFirstComma( ref line ).Replace( "\"", "" );

				if ( guid == "NULL" )
					continue;

				string name = "";
				for ( int j = 0; j < 5; j++ )
				{
					if ( j == 2 )
						name = SplitByFirstComma( ref line ).Replace( "\"", "" );
					else
						SplitByFirstComma( ref line );
				}



				if ( line.StartsWith( "," ) )
					line =  new string( line.Skip( 1 ).ToArray() );

				var geometry = new Geometry
				{
					NMObjectExternalId = new Guid( guid ),
					ExternalName= name,
					GeometryType = GetGeometryType( line )
				};
				if ( added.Contains( geometry.NMObjectExternalId ) )
					continue;

				added.Add( geometry.NMObjectExternalId );


				ret.Add( geometry );


				var geo = GetGeometryCoordinates( line ).Replace( Environment.NewLine, "" ).Replace( "\"", "" );
				var xy = geo.Split( ',' );
				var xylist = new List<GeometryCoordinate>();
				foreach ( var item in xy )
				{
					GeometryCoordinate geometryCoordinate = new GeometryCoordinate();
					geometryCoordinate.Latitude =  double.Parse( item.Split( ' ' )[0], CultureInfo.InvariantCulture );
					geometryCoordinate.Longitude =  double.Parse( item.Split( ' ' )[1] );
					xylist.Add( geometryCoordinate );
				};
				var a = xy.Select( x => new GeometryCoordinate
				{
					Latitude = double.Parse( x.Split( ' ' )[0] ),
					Longitude = double.Parse( x.Split( ' ' )[1] )
				} );

				geometry.GeometryCoordinates.AddRange(
					GetGeometryCoordinates( line )
							.Replace( Environment.NewLine, "" )
							.Replace( "\"", "" )
							.Split( ',' )
							.Select( x => new GeometryCoordinate
							{
								Latitude = double.Parse( x.Split( ' ' )[0], CultureInfo.InvariantCulture ),
								Longitude = double.Parse( x.Split( ' ' )[1], CultureInfo.InvariantCulture ),
							} ) );
			}

			return ret;
		}

		private string SplitByFirstComma( ref string line )
		{
			if ( line.StartsWith( "," ) )
				line = new string( line.Skip( 1 ).ToArray() );
			for ( int i = 0; i < line.Length; i++ )
			{
				if ( line[i] == ',' )
				{
					string oldLine = line;
					line = line.Substring( i, line.Length-1-i );
					return oldLine.Substring( 0, i );
				}
			}

			return line;
		}

		private GeometryType GetGeometryType( string data )
		{
			if ( data.ToUpper().Contains( "MULTILINESTRING" ) )
				return GeometryType.MULTILINESTRING;
			else if ( data.ToUpper().Contains( "LINESTRING" ) )
				return GeometryType.LINESTRING;
			else if ( data.ToUpper().Contains( "POINT" ) )
				return GeometryType.POINT;
			else
				return GeometryType.Unknown;

		}

		private string GetGeometryCoordinates( string data )
		{
			string ret = data;
			switch ( GetGeometryType( data ) )
			{
				case GeometryType.MULTILINESTRING:
					ret = data.Replace( "MULTILINESTRING", "" );
					break;
				case GeometryType.LINESTRING:
					ret = data.Replace( "LINESTRING", "" );
					break;
				case GeometryType.POINT:
					ret = data.Replace( "POINT", "" );
					break;
				default:
					return "";
			}

			return ret.Replace( "(", "" ).Replace( ")", "" );
		}
	}



}
