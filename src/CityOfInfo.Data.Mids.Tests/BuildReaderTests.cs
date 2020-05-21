﻿using CityOfInfo.Data.Mids.Saves;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CityOfInfo.Data.Mids.Tests
{

    public class BuildReaderTests
    {

        [Fact]
        public void CanReadBuild()
        {
            var reader = new StringReader("|MxDz;1442;655;1310;HEX;||78DA6594594F13511886CFA153A1A5A5AD2C652B500A94B5827A6FA28051694242D4CBA6916369D2B4E3748872E93F50D478E3020A2E37FE1FF7F8035C12C5E5AA7E9DF705899D74FACCBCE7DBE6FBCE4CF6C67CE8D5D99BA7946E3D53CA57ABB9D3CEBA6B5AB2EB6EDE2D56CA3E2547B39CBD9E9ECB9A9231999575DB38B915D731E582BB16C7D2BCB96ACA5593B95C2C95ECCA75E3A8F072A552CA2C160B6B6EB15C087A772BB631AB21EF72C9E46DD123FB37ABC6A9AE15ED9E05BB7825B350364E612397CD575DE36CA0AA6EA96354CE612D7FF553D5FC6A474A9CB354D32EF914B49E90DBE0912DF0B53AE4FB8B6B7B60CB0FF20F183C0ECEC4C52161A99440D357C72C6F2D142523605B1B1906A321F08D78F9E06BF99827CABC477F92ACA7E337F856ACFDF0D1FE4B1AB55C044735E652AFA5F91E7204EF821DB7C0AEDBE426D87D077C275E01C4F5053A11AFBB03ECFD8EDCBD17C031915B99A7B5DAE469FD0E590107AE9136385706B5141886AF0AD7E450EFE53AE2B5B0E68FF423C340021C1A2007C921309904D3E216A36F6C071992BBE0B167E47356F0927C017E10AF7676B2FD3E7D37419F54D9C96E747212294E20C5098D706296D8C6691B67E746D8B9912838D60E0E8B6D0F9E5EF74C29AC911F057D88D3D4C738638C93669C2EF14FD026419BF47F361382414E67F001A63BF1907C443E0627B7C86D3010E39B24D319664F3EF10DAB3FDF289F7992BD98666FA6B94B6352DF38EB1B677DD3AC6F86F58524C714734C31474AFC32D4327D8815176D96DAAC8FBBCB523C6AF2ABEF9EC90665B641996B504E3428271B9445EBE0AB505B6A585DB6B8288AF6944054DE6628B5CFC183AF82D23398EFD77F9AD6E7E5222D7BF31CF8E5B0FD38ECBF1DB6DFEF13F7E75F04F8E113|");
            var saveReader = new SaveReader(reader);
            var save = saveReader.Read();
            Assert.NotNull(save);
            Assert.NotNull(save.CompressionData);

            using (var stream = new CompressionDataStream(save.CompressionData))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var buildReader = new BuildReader(binaryReader);
                    var build = buildReader.Read();
                    Assert.NotNull(build);
                    Assert.NotNull(build.Archetype);
                    Assert.Equal("Class_Brute", build.Archetype.ClassName);
                    Assert.Equal("Brute", build.Archetype.DisplayName);

                    Assert.NotNull(build.EnhancedPowers);
                    Assert.Equal(38, build.EnhancedPowers.Count);

                    Assert.NotNull(build.PowerSets);
                    Assert.Equal(8, build.PowerSets.Count);

                    Assert.Equal(1.01f, build.Version);
                    Assert.Equal(23, build.LastPower);
                    Assert.True(build.UseOldSubpowerFields);
                    Assert.False(build.UseQualifiedNames);
                }
            }                
        }
    }
}
